
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

#pragma warning disable 1591

namespace Project.GoHorse.Models
{
  #region Designer generated code

  using System;
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Data.Items;
  using Sitecore.Data.Fields;
  using Sitecore.Data;
  
      
  /// <summary>Represents the "Sitecore Module" template.</summary>
  public partial class SitecoreModule : CustomItem
  {
    public static readonly ID TemplateID = ID.Parse("{FF278675-A306-4078-A9D0-D48B4820C57C}");

    public SitecoreModule(Item item) : base(item) {
    }

    public static class FieldIds {
      
    }
    
    public static SitecoreModule Create(Item item) 
    {
      return new SitecoreModule(item);
    }

    public static implicit operator Item (SitecoreModule item)
    {
      if (item == null)
      {
        return null;
      }

      return item.InnerItem;
    }

    public static explicit operator SitecoreModule(Item item)
    {
      if (item == null)
      {
        return null;
      }

      if (item.TemplateID != TemplateID)
      {
        return null;
      }

      return Create(item);
    }
  }
  
  #endregion
}

#pragma warning restore 1591
