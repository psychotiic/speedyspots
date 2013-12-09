namespace SpeedySpots.Business.Helpers
{
   using System;
   using System.ComponentModel;
   using System.Linq;

   public static class EnumExtensions
   {
      public static string GetDescription(this Enum value)
      {
         var descriptionAttribute = value.GetType()
                                         .GetField(value.ToString())
                                         .GetCustomAttributes(false).FirstOrDefault(a => a is DescriptionAttribute)
                                    as DescriptionAttribute;

         return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
      }
   }
}