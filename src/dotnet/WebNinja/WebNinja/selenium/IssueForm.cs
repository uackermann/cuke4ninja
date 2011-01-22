﻿using System;
using OpenQA.Selenium;
using WebNinja.webninja;

namespace WebNinja.selenium
{
    public class IssueForm : FormObject
    {
        private readonly UserRepository _repository;

        public IssueForm(IWebDriver driver, UserRepository repository) : base(driver)
        {
            _repository = repository;
        }

        public string ProjectName
        {
            set
            {
                SelectOption(By.Name("bug_data[Project]"), value);
            }
        }

        public string Title
        {
            set
            {
                Driver.FindElement(By.Name("bug_data[Summary]")).SendKeys(value);
            }
        }

        public string Description
        {
            set
            {
                Driver.FindElement(By.Name("bug_data[Description]")).SendKeys(value);
            }
        }

        public string Severity
        {
            set
            {
                SelectOption(By.Name("bug_data[Severity]"), value);
            }
        }

        public void AssignTo(string userName)
        {
            User user = _repository.FindByUserId(userName);
            String name = user == null ? userName : user.Name;
            SelectOption(By.Name("bug_data[Assign_To]"), name);
        }

        public override void Submit()
        {
            var div = Driver.FindElement(By.Id("addEditActionButtons"));
            div.FindElement(
                By.XPath("input[@type='submit']")).Click();
        }
    }
}