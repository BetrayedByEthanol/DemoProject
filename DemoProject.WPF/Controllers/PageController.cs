using DemoProject.Core.Models;
using DemoProject.Core.ViewModels;
using DemoProject.WPF.HTTP;
using DemoProject.WPF.Views;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Controls;

namespace DemoProject.WPF.Controllers
{
    public static class PageController
    {
        private static readonly Dictionary<string, BidirectionalViewModel> viewModels = new Dictionary<string, BidirectionalViewModel>();
        private static readonly string server = "http://localhost";

        public static Page switchTo(string pageName)
        {
            try
            {
                switch (pageName)
                {
                    case "Bugs":
                        return new HomePage();
                    default:
                        return null;
                }
            }
            catch (UnauthorizedAccessException ex) { throw; }
        }

        public static BidirectionalViewModel getViewModel(string name)
        {
            switch (name)
            {
                case "Bugs":
                    if (!viewModels.TryGetValue("BugReport", out var viewModel))
                    {
                        List<BugModel> models = JsonSerializer.Deserialize<List<BugModel>>(RequestManager.Get("/BugReport"));
                        viewModel = new BugReportViewModel(models);
                        viewModel.conntectToHubAsync(server, "BugHub");
                        viewModels.Add("BugReport", viewModel);
                    }
                    return viewModel;
                default:
                    return null;
            }
        }
    }
}
