const et = require('../../ResuMeta_NodeAPIs/NodePdfImporter/extractText.js')

test('extractTextByLines correctly extracts text from NewExample.pdf', () => {
    // Arrange
    const pdfData = {
        "Transcoder": "pdf2json@3.0.5 [https://github.com/modesty/pdf2json]",
        "Meta": {
            "PDFFormatVersion": "1.7",
            "IsAcroFormPresent": false,
            "IsXFAPresent": false,
            "Author": "Tester Testingson",
            "Creator": "Microsoft® Word 2016",
            "Producer": "Microsoft® Word 2016",
            "CreationDate": "D:20240314103933-07'00'",
            "ModDate": "D:20240314103933-07'00'",
            "Metadata": {
                "pdf:producer": "Microsoft® Word 2016",
                "dc:creator": "Tester Testingson",
                "xmp:creatortool": "Microsoft® Word 2016",
                "xmp:createdate": "2024-03-14T10:39:33-07:00",
                "xmp:modifydate": "2024-03-14T10:39:33-07:00",
                "xmpmm:documentid": "uuid:FakeUUID",
                "xmpmm:instanceid": "uuid:FakeUUID"
            }
        },
        "Pages": [
            {
                "Width": 38.25,
                "Height": 49.5,
                "HLines": [],
                "VLines": [],
                "Fills": [],
                "Texts": [
                    {
                        "x": 4.252,
                        "y": 4.449,
                        "w": 151.68,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "This%20is%20an%20example%20document%20",
                                "S": -1,
                                "TS": [
                                    0,
                                    16,
                                    1,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 13.726,
                        "y": 4.449,
                        "w": 39.336,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "used%20to%20",
                                "S": -1,
                                "TS": [
                                    0,
                                    16,
                                    1,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 16.186,
                        "y": 4.449,
                        "w": 20.988,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "test%20",
                                "S": -1,
                                "TS": [
                                    0,
                                    16,
                                    1,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 17.491,
                        "y": 4.449,
                        "w": 55.656,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "pdf%20service",
                                "S": -1,
                                "TS": [
                                    0,
                                    16,
                                    1,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 20.979,
                        "y": 4.449,
                        "w": 3,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "%20",
                                "S": -1,
                                "TS": [
                                    3,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 4.252,
                        "y": 5.881,
                        "w": 150.312,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "This%20should%20be%20able%20to%20read%20the%20",
                                "S": -1,
                                "TS": [
                                    0,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 13.643,
                        "y": 5.881,
                        "w": 47.328,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "document",
                                "S": -1,
                                "TS": [
                                    0,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 16.598,
                        "y": 5.881,
                        "w": 3,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "%20",
                                "S": -1,
                                "TS": [
                                    0,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 16.786,
                        "y": 5.881,
                        "w": 256.284,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "line%20by%20line%20and%20be%20able%20to%20reconstruct%20a%20message%20that%20",
                                "S": -1,
                                "TS": [
                                    0,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 4.252,
                        "y": 6.811,
                        "w": 230.316,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "holds%20some%20meaning%20into%20a%20json%20message%20format",
                                "S": -1,
                                "TS": [
                                    0,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 18.646,
                        "y": 6.811,
                        "w": 3,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "%20",
                                "S": -1,
                                "TS": [
                                    3,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 4.252,
                        "y": 8.244,
                        "w": 3,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "%20",
                                "S": -1,
                                "TS": [
                                    3,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    },
                    {
                        "x": 4.252,
                        "y": 9.669,
                        "w": 3,
                        "clr": 0,
                        "sw": 0.32553125,
                        "A": "left",
                        "R": [
                            {
                                "T": "%20",
                                "S": -1,
                                "TS": [
                                    3,
                                    15,
                                    0,
                                    0
                                ]
                            }
                        ]
                    }
                ],
                "Fields": [],
                "Boxsets": []
            }
        ]
    };
    const expectedJson = [[{ "text": "This is an example document ", "style": [0, 16, 1, 0] }, { "text": "used to ", "style": [0, 16, 1, 0] }, { "text": "test ", "style": [0, 16, 1, 0] }, { "text": "pdf service", "style": [0, 16, 1, 0] }, { "text": " ", "style": [3, 15, 0, 0] }], [{ "text": "This should be able to read the ", "style": [0, 15, 0, 0] }, { "text": "document", "style": [0, 15, 0, 0] }, { "text": " ", "style": [0, 15, 0, 0] }, { "text": "line by line and be able to reconstruct a message that ", "style": [0, 15, 0, 0] }], [{ "text": "holds some meaning into a json message format", "style": [0, 15, 0, 0] }, { "text": " ", "style": [3, 15, 0, 0] }], [{ "text": " ", "style": [3, 15, 0, 0] }], [{ "text": " ", "style": [3, 15, 0, 0] }]];

    // Act
    const actualJson = et.extractTextByLines(pdfData);

    // Assert 
    expect(actualJson).toEqual(expectedJson);
});
