const extractTextByLines = require('../../ResuMeta_NodeAPIs/NodePdfImporter/index')

test('extractTextByLines correctly extracts text from NewExample.pdf', () => {
    // Arrange
    const pdfPath = '../ExamplePDFs/NewExample.pdf'
    const expectedJson = [[{ "text": "This is an example document ", "style": [0, 16, 1, 0] }, { "text": "used to ", "style": [0, 16, 1, 0] }, { "text": "test ", "style": [0, 16, 1, 0] }, { "text": "pdf service", "style": [0, 16, 1, 0] }, { "text": " ", "style": [3, 15, 0, 0] }], [{ "text": "This should be able to read the ", "style": [0, 15, 0, 0] }, { "text": "document", "style": [0, 15, 0, 0] }, { "text": " ", "style": [0, 15, 0, 0] }, { "text": "line by line and be able to reconstruct a message that ", "style": [0, 15, 0, 0] }], [{ "text": "holds some meaning into a json message format", "style": [0, 15, 0, 0] }, { "text": " ", "style": [3, 15, 0, 0] }], [{ "text": " ", "style": [3, 15, 0, 0] }], [{ "text": " ", "style": [3, 15, 0, 0] }]];

    // Act
    const actualJson = extractTextByLines(pdfPath);

    // Assert 
    expect(actualJson).toEqual(expectedJson);
});








/*
[
    [
        {
            "text": "This is an example document ",
            "style": [
                0,
                16,
                1,
                0
            ]
        },
        {
            "text": "used to ",
            "style": [
                0,
                16,
                1,
                0
            ]
        },
        {
            "text": "test ",
            "style": [
                0,
                16,
                1,
                0
            ]
        },
        {
            "text": "pdf service",
            "style": [
                0,
                16,
                1,
                0
            ]
        },
        {
            "text": " ",
            "style": [
                3,
                15,
                0,
                0
            ]
        }
    ],
    [
        {
            "text": "This should be able to read the ",
            "style": [
                0,
                15,
                0,
                0
            ]
        },
        {
            "text": "document",
            "style": [
                0,
                15,
                0,
                0
            ]
        },
        {
            "text": " ",
            "style": [
                0,
                15,
                0,
                0
            ]
        },
        {
            "text": "line by line and be able to reconstruct a message that ",
            "style": [
                0,
                15,
                0,
                0
            ]
        }
    ],
    [
        {
            "text": "holds some meaning into a json message format",
            "style": [
                0,
                15,
                0,
                0
            ]
        },
        {
            "text": " ",
            "style": [
                3,
                15,
                0,
                0
            ]
        }
    ],
    [
        {
            "text": " ",
            "style": [
                3,
                15,
                0,
                0
            ]
        }
    ],
    [
        {
            "text": " ",
            "style": [
                3,
                15,
                0,
                0
            ]
        }
    ]
]



*/