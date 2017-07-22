using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChainOfResponsibility
{
    public partial class Form1 : Form
    {
        private ViewModel viewModel;
        public Form1()
        {
            InitializeComponent();

            ResetListBox();
        }

        private void ResetListBox()
        {
            button1.Enabled = true;
            button2.Enabled = false;
            viewModel = new ViewModel();
            InitialListBoxData(listBox1, viewModel.LeftRiver);
            InitialListBoxData(listBox2, viewModel.RightRiver);
            listBox3.Items.Clear();
        }
        private void InitialListBoxData(ListBox listbox, BindingSource binding)
        {
            listbox.DataSource = binding;
            listbox.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            GameStageResult result = new GameStageResult();
            var item = (Role)listBox1.SelectedItem;
            result.Route = viewModel.LeftToRight(item);
            button2.Enabled = true;
            Check(result);

            ShowResult(result);
            if (viewModel.IsGameClear())
            {
                listBox3.Items.Add("完成渡河 $$$");
                button2.Enabled = false;
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            GameStageResult result = new GameStageResult();
            var item = (Role)listBox2.SelectedItem;
            result.Route = viewModel.RightToLeft(item);
            Check(result);
            ShowResult(result);
            button1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetListBox();
            listBox3.Items.Clear();
        }

        private void ShowResult(GameStageResult result)
        {
            listBox3.Items.Add(result.Route);
            listBox3.Items.Add(result.Message);
            listBox3.Items.Add("--------------");
            if (result.State == GameState.Failed)
            {
                listBox3.Items.Add("渡河失敗 !!!");
            }
        }

        private void Check(GameStageResult result)
        {
            result.State = GameState.Running;
            var stageLeft = viewModel.HandelLeftGameFailed();
            StageCheck(result, stageLeft, "左邊");
            var stageRight = viewModel.HandelRightGameFailed();
            StageCheck(result, stageRight, "右邊");
        }

        private void StageCheck(GameStageResult result, Tuple<bool, IEnumerable<Role>> stage, string prefix)
        {
            if (stage.Item1)
            {
                result.State = GameState.Failed;
                foreach (var role in stage.Item2)
                {
                    result.Message += string.Format("{0} :{1} 吃掉 {2}", prefix, role.Name, role.Food);
                    result.Message += Environment.NewLine;
                }
                button2.Enabled = false;
            }

        }
    }
}
