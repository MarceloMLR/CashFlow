﻿using System.Collections;

namespace WebApi.Tests.InlineData
{
    public class CultureInlineDataTest : IEnumerable<Object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
           yield return new object[] {"pt-BR" };
           yield return new object[] {"en" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
