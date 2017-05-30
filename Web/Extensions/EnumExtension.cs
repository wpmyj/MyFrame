﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Web
{
    /// <summary>
    /// 枚举类扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举的显示文字
        /// </summary>
        /// <param name="value"></param>
        public static string GetDisplayName(this Enum value)
        {
            string name = Enum.GetName(value.GetType(), value);
            if (string.IsNullOrEmpty(name))
                return value.ToString();
            var attribute = value.GetType().GetField(name).GetCustomAttributes(
                 typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false)
                 .Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                 .FirstOrDefault();
            if (attribute != null)
                return attribute.Name;

            return value.ToString();
        }

        /// <summary>
        /// 根据枚举类型获取，枚举的selectlist，Text是Display name
        /// 有空选项
        /// </summary>
        /// <param name="valueEnum"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToListWithEmptyItem(this Enum valueEnum)
        {
            SelectListItem empty = new SelectListItem();
            empty.Text = "-请选择-";
            empty.Value = "";
            List<SelectListItem> listRes = new List<SelectListItem>();
            listRes.Add(empty);
            var lists = from Enum value in Enum.GetValues(valueEnum.GetType())
                        select new SelectListItem
                        {
                            Text = value.GetDisplayName(),//valueEnum.GetDisplayName(),
                            Value = value.GetHashCode().ToString()
                        };
            foreach (var list in lists)
            {
                listRes.Add(list);
            }
            return listRes.ToList();
        }

        /// <summary>
        /// 根据枚举类型获取，枚举的selectlist，Text是Display name
        /// 没有空选项
        /// </summary>
        /// <param name="valueEnum"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToListWithNoEmptyItem(this Enum valueEnum)
        {
            var lists = from Enum value in Enum.GetValues(valueEnum.GetType())
                        select new SelectListItem
                        {
                            Text = value.GetDisplayName(),//valueEnum.GetDisplayName(),
                            Value = value.GetHashCode().ToString()
                        };
            return lists.ToList();
        }

        /// <summary>
        /// 根据枚举类型获取，枚举的selectlist，Text是Display name
        /// 传入空选项文字
        /// </summary>
        /// <param name="valueEnum"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToListWithDesign(this Enum valueEnum, string noticeStr = "")
        {
            SelectListItem empty = new SelectListItem();
            empty.Text = noticeStr;
            empty.Value = "";
            List<SelectListItem> listRes = new List<SelectListItem>();
            listRes.Add(empty);
            var lists = from Enum value in Enum.GetValues(valueEnum.GetType())
                        select new SelectListItem
                        {
                            Text = value.GetDisplayName(),//valueEnum.GetDisplayName(),
                            Value = value.GetHashCode().ToString()
                        };
            foreach (var list in lists)
            {
                listRes.Add(list);
            }
            return listRes.ToList();
        }

        /// <summary>
        /// 根据枚举类型获取，枚举的selectlist，Text是Display name
        /// </summary>
        /// <param name="valueEnum"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItem(this Enum valueEnum, string selectName)
        {
            return (from int value in Enum.GetValues(valueEnum.GetType())
                    select new SelectListItem
                    {
                        Text = Enum.GetName(valueEnum.GetType(), value),
                        Value = Enum.GetName(valueEnum.GetType(), value),
                        Selected = Enum.GetName(valueEnum.GetType(), value) == selectName ? true : false
                    }).ToList();
        }
    }
}