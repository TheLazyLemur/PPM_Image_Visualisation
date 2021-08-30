using System;
using System.Diagnostics;
using System.IO;

const int width = 700;
const int height = 700;

var ppmData = new byte[width * height * 3];

Fill(ppmData);
CheckerPattern(ppmData, 100);
SolidCircle(ppmData, 350);

void Fill(byte[] pixelData)
{
    for (var x = 0; x < width; ++x)
    {
        for (var y = 0; y < height; ++y)
        {
            var currentPixel = (y * height + x) * 3;
            SetPixelColor(pixelData, currentPixel,255,0,255);
        }
    }

    WritePpm(pixelData, "Fill.ppm");
}

void SolidCircle(byte[] pixelData, int radius)
{
    const int centerX = width / 2;
    const int centerY = height / 2;

    for (var x = 0; x < width; ++x)
    {
        for (var y = 0; y < height; ++y)
        {
            var currentPixel = (y * height + x) * 3;
            var dx = centerX - x;
            var dy = centerY - y;
            if (dx * dx + dy * dy <= radius * radius)
                SetPixelColor(pixelData, currentPixel,0,0,0);
            else
                SetPixelColor(pixelData, currentPixel,255,0,255);
        }
    }

    WritePpm(pixelData, "SolidCircle.ppm");
}

void CheckerPattern(byte[] pixelData, int tileSize)
{
    for (var x = 0; x < width; ++x)
    {
        for (var y = 0; y < height; ++y)
        {
            var currentPixel = (y * height + x) * 3;
            if ((x / tileSize + y / tileSize) % 2 == 0)
                SetPixelColor(pixelData, currentPixel, 0, 0, 0);
            else
                SetPixelColor(pixelData, currentPixel, 255, 0, 255);
        }
    }

    WritePpm(pixelData, "CheckerPattern.ppm");
}

void SetPixelColor(byte[] pixelData, int currentPixel, byte r, byte g, byte b)
{
    pixelData[currentPixel] = r;
    pixelData[currentPixel + 1] = g;
    pixelData[currentPixel + 2] = b;
}

void WritePpm(byte[] pixelData, string name)
{
    var destination = new StreamWriter($"./{name}");
    destination.Write("P6\n{0} {1}\n{2}\n", width, height, 255);
    destination.Flush();
    destination.BaseStream.Write(pixelData, 0, pixelData.Length);
    destination.Close();
    Console.WriteLine($"Saved {name}");
}