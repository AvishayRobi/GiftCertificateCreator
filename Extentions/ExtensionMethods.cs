using System;
using System.Collections.Generic;

namespace GiftCertificateCreator.Extentions
{
  public static class ExtensionMethods
  {
    public static void ApplyEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
      foreach (T item in enumeration)
      {
        action(item);
      }
    }
  }
}
