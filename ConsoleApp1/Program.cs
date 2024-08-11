using SimplePromise;

var promise = new Promise<string>((resolve, reject) =>
{
    resolve("Hello World");
});
promise.Then(
    (res) =>
    {
        Console.WriteLine("Success:" + res + " 1");
    },
    (err) =>
    {
        Console.WriteLine("Error:" + err);
    }).Then(
    (res) =>
    {
        Console.WriteLine("Success:" + res + " 2");
    },
    (err) =>
    {
        Console.WriteLine("Error:" + err); 
    });