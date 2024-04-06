import { containsEmptyInput } from '../../ResuMeta/wwwroot/js/CreateResume_Modules/utility-mod.js';

test('inputting empty string outputs true', () => {
    expect(containsEmptyInput("")).toBe(true);
});

test('inputting non-empty string outputs false', () => {
    expect(containsEmptyInput("non-empty string")).toBe(false);
});

test('inputting string with only spaces outputs false', () => {
    expect(containsEmptyInput("   ")).toBe(false);
});