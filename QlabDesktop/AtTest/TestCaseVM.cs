using Reactive.Bindings;
using System;

namespace QlabDesktop.AtTest
{
    public class TestCaseVM
    {
        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> In { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> Out { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> Actual { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> JudgeText { get; } = new ReactiveProperty<string>();

        internal void CheckTest(string data)
        {
            if (Out.Value == data)
            {
                JudgeText.Value = "Pass";
            }
            else
            {
                JudgeText.Value = "WA";
            }
            Actual.Value = data;
        }
    }
}