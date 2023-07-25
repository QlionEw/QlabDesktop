using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlabDesktop.AtTest
{
    internal class AtTestModel
    {
        private readonly string AtCoderToolsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "atcoder-workspace");
        private AtTestViewModel viewModel;
        private Dictionary<string, QuestionModel> questionModelDict = new Dictionary<string, QuestionModel>();
        private Dictionary<string, QuestionPanelVM> questionDict = new Dictionary<string, QuestionPanelVM>();

        public AtTestModel(AtTestViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.Selected.Subscribe(OnSelectContest);
            viewModel.SelectedQuestion.Subscribe(OnSelectedQuestion);

            UpdateContestList();
        }

        private void OnSelectedQuestion(QuestionPanelVM vm)
        {
            if (vm == null) return;
            var model = vm.Model;
            model.LoadTestCases();
        }

        private void UpdateContestList()
        {
            foreach (var name in Directory.EnumerateDirectories(AtCoderToolsDirectory))
            {
                viewModel.AddContest(name.Split('\\')[^1]);
            }
        }

        private void OnSelectContest(ContestItem item)
        {
            viewModel.Questions.Clear();
            if (item == null)
            {
                return;
            }
            string ContestPath = Path.Combine(AtCoderToolsDirectory, item.Id);
            foreach (var namePath in Directory.EnumerateDirectories(ContestPath))
            {
                var name = namePath.Split('\\')[^1];
                var key = $"{item.Id}_{name}";
                if (questionDict.TryGetValue(key, out var question))
                {
                    viewModel.Questions.Add(question);
                }
                else
                {
                    var questionVm = new QuestionPanelVM();
                    questionVm.Name.Value = name;
                    var model = new QuestionModel(questionVm, namePath);
                    questionVm.Model = model;
                    questionDict.Add(key, questionVm);
                    viewModel.Questions.Add(questionVm);
                }
            }
            viewModel.SelectedQuestion.Value = viewModel.Questions.First();
        }
    }
}
