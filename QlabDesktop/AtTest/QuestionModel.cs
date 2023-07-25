using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlabDesktop.AtTest
{
    internal class QuestionModel
    {
        private string Exe = "AtCoder.exe";
        private bool isLoaded = false;
        private QuestionPanelVM viewModel;
        private string questionPath;

        public QuestionModel(QuestionPanelVM viewModel, string questionPath)
        {
            this.viewModel = viewModel;
            this.questionPath = questionPath;

            viewModel.TestAllCommand.Subscribe(TestAll);
        }

        private void TestAll()
        {
            foreach (var testCase in viewModel.TestCases)
            {
                Task.Run(() => Test(testCase));
            }
        }

        private async Task<bool> Test(TestCaseVM testCase)
        {
            try
            {
                var command = new Process();
                command.StartInfo.WorkingDirectory = questionPath;
                command.StartInfo.FileName = Path.Combine(questionPath, Exe);
                command.StartInfo.RedirectStandardInput = true;
                command.StartInfo.RedirectStandardOutput = true;
                command.StartInfo.CreateNoWindow = true;

                command.Start();
                command.StandardInput.Write(testCase.In);
                command.ErrorDataReceived += (sender, e) => testCase.JudgeText.Value = "RE";
                await command.WaitForExitAsync();

                testCase.CheckTest(command.StandardOutput.ReadToEnd());

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        internal void LoadTestCases()
        {
            if (isLoaded)
            {
                return;
            }
            foreach (var file in Directory.EnumerateFiles(questionPath, "in_*.txt"))
            {
                var fileName = file.Split("\\").Last().Replace("in_", "").Replace(".txt", "");
                var testCase = new TestCaseVM();
                testCase.Name.Value = fileName;
                using (var intxt = new StreamReader(Path.Combine(questionPath, $"in_{fileName}.txt")))
                {
                    testCase.In.Value = intxt.ReadToEnd();
                }
                using (var outtxt = new StreamReader(Path.Combine(questionPath, $"out_{fileName}.txt")))
                {
                    testCase.Out.Value = outtxt.ReadToEnd();
                }
                viewModel.TestCases.Add(testCase);
            }
            isLoaded = true;
        }
    }
}
