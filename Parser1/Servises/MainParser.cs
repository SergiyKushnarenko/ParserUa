﻿using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using Parser1.Helpers;
using Parser1.Interfaces;
using DAL.Models;
using DAL.Repositories;
using DAL.AdditionalModels;

namespace Parser1.Servises
{
    public class MainParser : IMainParser
    {
        private readonly IWebDriver _driver;
        private readonly IDirectionRepository _directionRepository;
        private readonly IScientistRepository _scientistRepository;
        private readonly ISupportParser _supportParser;
        private readonly IRatingServise _ratingServise;
        private const string direction = "Педагогіка";
        private const string url = @"http://nbuviap.gov.ua/bpnu/index.php?page=search";
        public MainParser(
            ISupportParser supportParser,
            IRatingServise ratingServise,
            IWebDriver driver,
            IDirectionRepository directionRepository,
            IScientistRepository scientistRepository)
        {
            _supportParser = supportParser;
            _ratingServise = ratingServise;
            _driver = driver;
            _directionRepository = directionRepository;            _scientistRepository = scientistRepository;

        }

        /// <summary>
        /// the main parser 
        /// </summary>
        /// <returns></returns>
        public async Task ParseGeneralInfo()
        {
            _driver.Url = url;
            _driver.FindElement(By.XPath("//select[@name='galuz1']")).SendKeys(direction);

            await Task.Delay(500);

            try
            {
                _driver.FindElement(By.XPath("//input[@class='btn btn-primary mb-2']")).Click();

                await Task.Delay(4000);

                while (true)
                {
                    var currentDirection = _driver.FindElement(By.XPath("/html/body/main/div[1]/div[1]/div/div/div/table/tbody/tr/td[5]"));

                    var names = _driver.FindElements(By.XPath("/html/body/main/div[1]/table/tbody/tr/td[3]"));

                    var organization = _driver
                        .FindElements(By.XPath("/html/body/main/div[1]/table/tbody/tr/td[8]"));

                    var dirtySubdirectionOfWork = _driver.FindElements(By.XPath($"//table/tbody/tr/td[contains(.,'{direction}')]")).Select(e => e.Text).ToList();

                    var subdirectionOfWork = StrHelper.GetListSubdirection(dirtySubdirectionOfWork);



                    await Task.Delay(500);

                    var directionId = (await _directionRepository.GetAsync(currentDirection.Text))!.Id;

                    for (int i = 0; i < names.Count; i++)
                    {
                        var rating = _ratingServise.GerRatingGoogleScholar(names[i].Text);

                        var ListOfSocial = _supportParser.GetSocialNetwork(names[i].Text);

                        var listOfWorkWithDegree = _supportParser.GetListOfWork(names[i].Text);

                        var degree = listOfWorkWithDegree.degree;

                        var scientist = new Scientist()
                        {
                            Name = names.ElementAt(i).Text,
                            // Organization = organization.ElementAt(i).Text,
                            DirectionId = directionId,
                            Rating = rating,
                            Degree = degree,


                        };
                        var foundResult = await _scientistRepository.GetAsync(scientist.Name) is not null;

                        if (foundResult)
                        {

                            await _scientistRepository.CreateAsync(scientist);
                            _supportParser.AddWorkToScientist(scientist.Name, listOfWorkWithDegree.Item1);
                            _supportParser.AddScietistSubdirAndAddDirectionToDb(subdirectionOfWork, direction, scientist.Name);
                            _supportParser.AddSocialNetworkToScientist(ListOfSocial, scientist.Name);
                        }
                    }

                    try
                    {
                        _driver.FindElement(By.XPath("//a[contains(.,'>>')]")).Click();
                    }
                    catch (OpenQA.Selenium.NoSuchElementException e)
                    {
                        break;
                    }
                }


                _supportParser.AddWorkToScientists("педагогічні науки");
            }
            catch (Exception)
            {
                // driver.Quit();
                _driver.Close();
                _supportParser.GetGeneralInfo("педагогічні науки", "Педагогіка");
            }

            _driver.Quit();
        }

        /// <summary>
        /// Get and check current directions 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetDirection()
        {
            _driver.Url = @"http://nbuviap.gov.ua/bpnu/index.php?page=search";

            var directionsElements = _driver.FindElements(By.XPath("//*[@id=\"galuz1\"]/option"));
            var foundDirections = new List<string>();

            foreach (var direction in directionsElements)
            {
                if (string.IsNullOrEmpty(direction.Text))
                {
                    continue;
                }
                foundDirections.Add(direction.Text);
            }

            if (foundDirections.Count == await _directionRepository.GetCountAsync())
                return foundDirections;

            var existingDirections = await _directionRepository.GetDirectionsAsync();
            var nonExistingDirections = foundDirections
                .Where(foundDirection => !existingDirections.Any(existingDirection => existingDirection.Name.Equals(foundDirection)));

            await _directionRepository.CreateAsync(nonExistingDirections.Select(directionName => new Direction()
            {
                Name = directionName
            }));

            return foundDirections;
        }

        /// <summary>
        /// Check current count Scientists on DB and Site 
        /// </summary>
        /// <param name="direction"></param>
        public async Task CheckOnEquals(string direction)
        {
            int directionId;

            _driver.Url = @"http://nbuviap.gov.ua/bpnu/index.php?page=search";

            _driver.FindElement(By.XPath("//select[@name='galuz1']")).SendKeys(direction);

            await Task.Delay(500);

            _driver.FindElement(By.XPath("//input[@class='btn btn-primary mb-2']")).Click();

            await Task.Delay(4000);

            var totalCount = _driver.FindElement(By.XPath("/html/body/main/div[1]/div[2]/div/div"));
            var totalSumOnSite = StrHelper.GetSumFromString(totalCount.Text);

            try
            {
                directionId = (await _directionRepository.GetAsync(direction))!.Id;
                var scientistsCount = await _scientistRepository.GetScientistsCountAsync(new ScientistFilter { DirectionId = directionId });

                if (totalSumOnSite != scientistsCount)
                {
                    await ParseNewScientist(direction);
                }
            }
            catch (Exception e)
            {
                //TODO
                string a = $"{e.Message} такого направления нет ";
                throw;
            }
            _driver.Quit();
        }

        /// <summary>
        /// Parse new Scientists on current direction
        /// </summary>
        /// <param name="direction"></param>
        public async Task ParseNewScientist(string direction)
        {
            _driver.Url = @"http://nbuviap.gov.ua/bpnu/index.php?page=search";

            _driver.FindElement(By.XPath("//select[@name='galuz1']")).SendKeys(direction);

            await Task.Delay(500);


            _driver.FindElement(By.XPath("//input[@class='btn btn-primary mb-2']")).Click();

            await Task.Delay(4000);
            var existingScientists = await _scientistRepository.GetScientistsListAsync();
            var existingDirections = await _directionRepository.GetDirectionsAsync();
            var scientistsToCreate = new List<Scientist>();
            while (true)
            {
                var currentDirection = _driver.FindElement(By.XPath("/html/body/main/div[1]/div[1]/div/div/div/table/tbody/tr/td[5]"));

                var scientistsNames = _driver.FindElements(By.XPath("/html/body/main/div[1]/table/tbody/tr/td[3]")).ToList(); //.GetAttribute("textContent");

                var organization = _driver
                    .FindElements(By.XPath("/html/body/main/div[1]/table/tbody/tr/td[8]"));

                await Task.Delay(500);
                
                var directionId = existingDirections.FirstOrDefault(existingDirection => existingDirection.Name.Equals(currentDirection.Text))?.Id;
                if (directionId != null)
                {
                    //TODO
                    // А что делать если ее в базе нет?))
                }

                scientistsNames.ForEach(scientistName => 
                {
                    if (!existingScientists.Any(existingScientist => existingScientist.Name.Equals(scientistName.Text)))
                    {
                        scientistsToCreate.Add(new Scientist()
                        {
                            Name = scientistName.Text,
                            //Organization = organization.ElementAt(i).Text,
                            DirectionId = directionId.Value,
                        });
                    }
                });

                try
                {
                    _driver.FindElement(By.XPath("//a[contains(.,'>>')]")).Click();
                }
                catch (OpenQA.Selenium.NoSuchElementException e)
                {
                    break;
                }
            }
            await _scientistRepository.CreateAsync(scientistsToCreate);
            _driver.Quit();
        }


    }
}
