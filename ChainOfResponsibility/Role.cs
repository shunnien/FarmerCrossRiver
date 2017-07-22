using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility
{
    /// <summary>
    /// 角色的列舉值，為了增加趣味性，所以我們用中文
    /// </summary>
    public enum RoleName
    {
        None,
        農夫,
        狼,
        羊,
        菜
    }

    /// <summary>
    /// 角色定義
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets the name. 角色本人
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public RoleName Name
        { get; private set; }

        /// <summary>
        /// Gets the food. 角色會吃的對象
        /// </summary>
        /// <value>
        /// The food.
        /// </value>
        public RoleName Food
        { get; private set; }

        public Role(RoleName name, RoleName food)
        {
            Name = name;
            Food = food;
        }

        /// <summary>
        /// 為了一點小小趣味, 這個方法在判斷傳入值是否會被角色本人吃掉
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Eat(Role other)
        {
            return (Food != RoleName.None && Food == other.Name);
        }

        /// <summary>
        /// Gets the default collection. (遊戲起始的集合)
        /// </summary>
        /// <returns></returns>
        public static List<Role> GetDefaultCollection()
        {
            var result = new List<Role>();
            result.Add(new Role(RoleName.農夫, RoleName.None));
            result.Add(new Role(RoleName.狼, RoleName.羊));
            result.Add(new Role(RoleName.羊, RoleName.菜));
            result.Add(new Role(RoleName.菜, RoleName.None));
            return result;
        }
    }
}
