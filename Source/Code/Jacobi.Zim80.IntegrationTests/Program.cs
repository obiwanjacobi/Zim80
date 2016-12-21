using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Jacobi.Zim80.IntegrationTests
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length < 1 || args.Length > 2)
            {
                Console.WriteLine("Invalid args. Expect Test Class name and/or Test Method name.");
                return 0;
            }

            TestLoader loader;
            if (args.Length == 1)
                loader = new TestLoader(args[0]);
            else
                loader = new TestLoader(args[0], args[1]);

            var runner = new TestRunner(loader.TestMethod);

            try
            {
                runner.Run();
            }
            catch(AssertFailedException afe)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(afe.ToString());
            }
            catch(AssertInconclusiveException aie)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(aie.ToString());
            }

            Console.WriteLine("Done.");
            Console.ReadLine();

            return 0;
        }
    }

    internal class TestRunner
    {
        private readonly MethodInfo _testMethod;

        public TestRunner(MethodInfo testMethod)
        {
            _testMethod = testMethod;
        }

        public void Run()
        {
            var testInstance = CreateTestObject();
            _testMethod.Invoke(testInstance, null);
        }

        private object CreateTestObject()
        {
            var t = _testMethod.DeclaringType;
            var instance = Activator.CreateInstance(t);

            SetTestContext(instance);

            return instance;
        }

        private void SetTestContext(object instance)
        {
            var t = _testMethod.DeclaringType;
            var p = t.GetProperty("TestContext", BindingFlags.Instance | BindingFlags.Public);
            if (p != null)
            {
                var dir = GetDeploymentDirectory();
                var ctx = new MinimalTestContext(dir);
                p.SetMethod.Invoke(instance, new[] { ctx });
            }
        }

        private string GetDeploymentDirectory()
        {
            var t = _testMethod.DeclaringType;
            var dia = (DeploymentItemAttribute)t.GetCustomAttribute(typeof(DeploymentItemAttribute));
            if (dia != null)
            {
                return Path.Combine(Environment.CurrentDirectory, Path.GetDirectoryName(dia.Path));
            }

            return Environment.CurrentDirectory;
        }
    }

    internal class MinimalTestContext : TestContext
    {
        private readonly Dictionary<object, object> _properties = new Dictionary<object, object>();
        private readonly string _deploymentDir;

        public MinimalTestContext(string deploymentDir)
        {
            _deploymentDir = deploymentDir;
        }

        public override string DeploymentDirectory
        {
            get { return _deploymentDir; }
        }
        
        public override System.Data.Common.DbConnection DataConnection
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Data.DataRow DataRow
        {
            get { throw new NotImplementedException(); }
        }

        public override IDictionary Properties
        {
            get { return _properties; }
        }

        public override void AddResultFile(string fileName)
        { }

        public override void BeginTimer(string timerName)
        { }

        public override void EndTimer(string timerName)
        { }

        public override void WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }
    }

    internal class TestLoader
    {
        private ClassFinder _finder = new ClassFinder(typeof(TestLoader).Assembly.GetTypes())
        {
            ClassAttribute = typeof(TestClassAttribute)
        };

        public TestLoader(string testMethodName)
        {
            TestMethod = FindTest(testMethodName);
        }

        public TestLoader(string testClassName, string testMethodName)
        {
            _finder.ClassName = testClassName;
            TestMethod = FindTest(testMethodName);
        }

        public MethodInfo TestMethod { get; private set; }

        private MethodInfo FindTest(string testMethod)
        {
            foreach(var tc in _finder)
            {
                var m = tc.GetMethod(testMethod, BindingFlags.Public | BindingFlags.Instance);
                if (m != null) return m;
            }

            return null;
        }
    }

    internal class ClassFinder : IEnumerable<Type>
    {
        private readonly IEnumerable<Type> _types;

        public ClassFinder(IEnumerable<Type> types)
        {
            _types = types;
        }

        public Type ClassAttribute { get; set; }

        public string ClassName { get; set; }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var t in _types)
            {
                if (!t.IsClass) continue;
                if (t.IsAbstract) continue;
                if (!t.IsPublic) continue;
                
                bool classNameFound = true;
                bool classAttrFound = true;

                if (!String.IsNullOrEmpty(ClassName))
                    if (ClassName.Contains('.'))
                        classNameFound = String.CompareOrdinal(t.FullName, ClassName) == 0;
                    else
                        classNameFound = String.CompareOrdinal(t.Name, ClassName) == 0;

                if (ClassAttribute != null)
                    classAttrFound = t.GetCustomAttribute(ClassAttribute) != null;

                if (classAttrFound && classNameFound)
                    yield return t;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
