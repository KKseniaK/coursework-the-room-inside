using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    [Test]
    public void NewTestScriptSimplePasses()
    {
        int a = 2 + 2;
        Assert.AreEqual(4, a, "Сложение работает неверно.");
    }

}
