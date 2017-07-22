using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChainOfResponsibility
{
    public class ViewModel
    {
        private List<Role> _leftData;
        private List<Role> _rightData;

        public BindingSource LeftRiver
        { get; private set; }

        public BindingSource RightRiver
        { get; private set; }

        public ViewModel()
        {
            _leftData = Role.GetDefaultCollection();
            _rightData = new List<Role>();
            LeftRiver = new BindingSource() { DataSource = _leftData };
            RightRiver = new BindingSource { DataSource = _rightData };
        }

        /// <summary>
        /// Resets all bindings. (集合變動時要反映到 Binding 到這些資料的對象)
        /// </summary>
        private void ResetAllBindings()
        {
            LeftRiver.ResetBindings(false);
            RightRiver.ResetBindings(false);
        }

        /// <summary>
        /// 把 item 從 source 移到 data
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="item">The item.</param>
        //private void MoveItem(Role item, List<Role> source, List<Role> target)
        //{
        //    source.Remove(item);
        //    target.Add(item);
        //}


        /// <summary>
        /// Lefts to right. (從左邊到右邊)
        /// </summary>
        /// <param name="selected">The selected.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public string LeftToRight(Role selected)
        {

            if (selected == null)
            {
                throw new ArgumentNullException();
            }


            string suffix = "從左邊到右邊";
            return Move(selected, _leftData, _rightData, suffix);

        }


        /// <summary>
        /// Rights to left. (從右邊到左邊)
        /// </summary>
        /// <param name="selected">The selected.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public string RightToLeft(Role selected)
        {

            if (selected == null)
            {
                throw new ArgumentNullException();
            }

            string suffix = "從右邊到左邊";
            return Move(selected, _rightData, _leftData, suffix);

        }


        /// <summary>
        /// Moves the specified selected form source to target
        /// </summary>
        /// <param name="selected">The selected.</param>
        /// <param name="suffix">The suffix.</param>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        private string Move(Role selected, List<Role> source, List<Role> target, string suffix)
        {
            //string result = string.Empty;
            //if (selected.Name == RoleName.農夫)
            //{
            //    MoveItem(selected, source, target);
            //    result = GetRouteString(selected, suffix);
            //}
            //else
            //{
            //    var farmer = source.FindFarmer();
            //    if (farmer != null)
            //    {
            //        MoveItem(farmer, source, target);
            //        MoveItem(selected, source, target);
            //        result = GetRouteString(farmer, selected, suffix);
            //    }
            //}
            //ResetAllBindings();
            //return result;

            var moveContext = new MoveContext(suffix);
            var result = moveContext.Move(selected, source, target);

            ResetAllBindings();
            return result;
        }




        /// <summary>
        /// Gets the route string. 取得農夫一人過河的路徑結果字串
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        //private string GetRouteString(Role owner, string suffix)
        //{
        //    return string.Format("{0} {1}", owner.Name, suffix);
        //}

        /// <summary>
        /// Gets the route string. 取得農夫帶著東西過河的路徑結果字串
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="selected">The selected.</param>
        /// <param name="suffix">The suffix.</param>
        /// <returns></returns>
        //private string GetRouteString(Role owner, Role selected, string suffix)
        //{
        //    return string.Format("{0} 帶著 {1} {2}", owner.Name, selected.Name, suffix);
        //}

        public bool IsGameClear()
        {
            return _leftData.IsGameClear();
        }

        /// <summary>
        /// (判斷左邊的集合是否遊戲失敗)
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, IEnumerable<Role>> HandelLeftGameFailed()
        {
            var result = _leftData.HandleGameFailed();
            return result;
        }
        /// <summary>
        /// (判斷右邊的集合是否遊戲失敗)
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, IEnumerable<Role>> HandelRightGameFailed()
        {
            var result = _rightData.HandleGameFailed();
            return result;
        }
    }
}
