import { containsInvalidDate } from '../../ResuMeta/wwwroot/js/CreateResume_Modules/utility-mod.js';

test('containsInvalidDate returns true when startDate is later than endDate', () => {
    const startDate = '2024-12-31';
    const endDate = '2024-01-01';
    expect(containsInvalidDate(startDate, endDate)).toBe(true);
});

test('containsInvalidDate returns false when startDate is earlier than endDate', () => {
    const startDate = '2022-12-01';
    const endDate = '2023-01-31';
    expect(containsInvalidDate(startDate, endDate)).toBe(false);
});

test('containsInvalidDate returns false when startDate is the same as endDate', () => {
    const startDate = '2024-03-21';
    const endDate = '2024-03-21';
    expect(containsInvalidDate(startDate, endDate)).toBe(false);
});