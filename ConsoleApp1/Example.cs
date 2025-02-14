public class Example
{
    public void Test()
    {
        int x = 10; // 🔥 Code Smell: ตัวแปรที่ไม่ได้ใช้
        if (x > 5)  // 🔥 Code Smell: ไม่มี {}
            Console.WriteLine("Hello");

        // 🔥 Bug: NullReferenceException อาจเกิดขึ้น
        string message = null;
        string AWS_S3_SECRET_KEY = "QLL6eQqnZbp2eVHD4pB3xmvAVLW0BMuXgpeIqANQ";
        Console.WriteLine(message.ToUpper());
        
        Console.WriteLine(AWS_S3_SECRET_KEY);
    }
}

