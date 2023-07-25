using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlabDesktop.AtTest
{
    internal class QuestionPanelVM
    {
        public QuestionModel Model { get; set; } //HACK: 構造が変
        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();
        public ReactiveCollection<TestCaseVM> TestCases { get; } = new ReactiveCollection<TestCaseVM>();
        public ReactiveProperty<TestCaseVM> SelectedCase { get; } = new ReactiveProperty<TestCaseVM>();
        public ReactiveCommand TestAllCommand { get; } = new ReactiveCommand();
    }
}
