using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QlabDesktop.AtTest
{
    public class ContestItem
    {
        public string Id { get; set; }

        public ContestItem(string id)
        {
            Id = id;
        }
    }

    internal class AtTestViewModel
    {
        public ReactiveCollection<ContestItem> Contests { get; } = new ReactiveCollection<ContestItem>();
        public ReactiveProperty<ContestItem> Selected { get; } = new ReactiveProperty<ContestItem>();
        public ReactiveCollection<QuestionPanelVM> Questions { get; } = new ReactiveCollection<QuestionPanelVM>();
        public ReactiveProperty<QuestionPanelVM> SelectedQuestion { get; } = new ReactiveProperty<QuestionPanelVM>();

        internal void AddContest(string name)
        {
            Contests.Add(new ContestItem(name));
        }
    }
}
