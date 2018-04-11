
## Azure Functions V2 Reference Issue Reproduction

This is a minimal reproduction that demonstrates a simple Azure Functions V2 function that throws an exception if the `WindowsAzure.Storage` NuGet package is referenced and used.

Run the `Functions` project as-is in Debug mode and the following error will occur:

```
System.Private.CoreLib: Exception while executing function: 
TestFunction. Functions: Could not load file or assembly 'Microsoft.WindowsAzure.Storage, 
Version=9.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'. 
Could not find or load a specific file. (Exception from HRESULT: 0x80131621). 
System.Private.CoreLib: Could not load file or assembly 'Microsoft.WindowsAzure.Storage, 
Version=9.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'.
```

This error suggests that the 'Microsoft.WindowsAzure.Storage' assembly cannot be found, even though it is in the same bin folder as the other binaries.

If the usage of `Microsoft.WindowsAzure.Storage` is bypassed by commenting out the functional line of the `TestFunction` function:

```csharp
        [FunctionName("TestFunction")]
        public static void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            //AccessCloudStorage(log);
        }
```

then, when the project is run in Debug mode, no errors occur.

### Conclusion

This same issue exists for other NuGet packages added to the Functions project (e.g. Autofac).  The `WindowsAzure.Storage` package was chosen for demonstration to show that it is not a non-Microsoft package issue - standard Microsoft packages cannot be found by the Azure Functions engine.

