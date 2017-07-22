using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    public static class RoleCollectionExtension
    {
        /// <summary>
        /// 判斷遊戲是否順利結束 (判斷初始集合的 count 是否為 0)
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static bool IsGameClear(this IEnumerable<Role> roles)
        {
            return (roles.Count() == 0);
        }

        /// <summary>
        /// 在集合中找到農夫
        /// </summary>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static Role FindFarmer(this IEnumerable<Role> roles)
        {
            return roles.FirstOrDefault((x) => x.Name == RoleName.農夫);
        }

        /// <summary>
        /// Handles the game failed. (判斷 game 是否失敗)
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <returns>Tuple: bool 為 true 代表遊戲失敗,IEnumerable<Role> 代表有吃掉東西的角色 </returns>
        public static Tuple<bool, IEnumerable<Role>> HandleGameFailed(this IEnumerable<Role> roles)
        {
            if (FindFarmer(roles) == null)
            {
                var result = roles.Where((x) => x.Food != RoleName.None && roles.Any((y) => x.Eat(y)));
                if (result.Count() > 0)
                {
                    return Tuple.Create<bool, IEnumerable<Role>>(true, result);
                }
            }

            return Tuple.Create<bool, IEnumerable<Role>>(false, null);
        }
    }
}
