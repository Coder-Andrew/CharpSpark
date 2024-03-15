import { isPdfExtension } from '../../ResuMeta/wwwroot/js/ResumeIndex_Modules/utility-mod.js';

test('Test if isPdfExtension will return false is extension isnt pdf', () => {
    expect(isPdfExtension("test.txt")).toBe(false);
    expect(isPdfExtension("obj.o")).toBe(false);
});

test('Test if isPdfExtension will return true is extension is pdf', () => {    
    expect(isPdfExtension("test.pdf")).toBe(true);
    expect(isPdfExtension("obj.pdf")).toBe(true);
    expect(isPdfExtension("Resume.pdf")).toBe(true);
});