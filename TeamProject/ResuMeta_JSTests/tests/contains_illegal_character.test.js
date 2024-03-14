import { containsIllegalCharacter } from '../../ResuMeta/wwwroot/js/CreateResume_Modules/utility-mod.js';

test('inputting \'hello\' outputs false', () => {
    expect(containsIllegalCharacter("Hello")).toBe(false);
});

test('inputting \'<script>Alert(\'this is a XSS exploit\')</script>\' outputs true', () => {
    expect(containsIllegalCharacter("<script>Alert('this is a XSS exploit')</script>")).toBe(true);
});

// technically '#' could be used for SQL injection, but it's not in the list of illegal characters
// as we want to allow the user to input programming languages
test('inputting \'C#\' outputs false', () => {
    expect(containsIllegalCharacter("C#")).toBe(false);
});

test('inputting \'C++\' outputs false', () => {
    expect(containsIllegalCharacter("C++")).toBe(false);
});


test('inputting SQL Injection string outputs true', () => {
    expect(containsIllegalCharacter("'; DROP TABLE users; --")).toBe(true);
});

test('inputting XSS string outputs true', () => {
    expect(containsIllegalCharacter("<img src='x' onerror='alert(\"XSS\")'>")).toBe(true);
});

test('inputting another SQL Injection string outputs true', () => {
    expect(containsIllegalCharacter("' OR '1'='1")).toBe(true);
});

test('inputting piggy-backed SQL Injection string outputs true', () => {
    expect(containsIllegalCharacter("'; INSERT INTO users (id, name) VALUES (1, 'hacker') --")).toBe(true);
});

test('inputting stored procedure SQL Injection string outputs true', () => {
    expect(containsIllegalCharacter("'; EXEC xp_cmdshell 'cat /etc/passwd' --")).toBe(true);
});

test('inputting union based SQL Injection string outputs true', () => {
    expect(containsIllegalCharacter("' UNION SELECT * FROM users --")).toBe(true);
});