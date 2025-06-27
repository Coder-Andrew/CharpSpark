export function basePathHelper(path) {
    const cleanPath = path.startsWith('/') ? path.slice(1) : path;
    const basePath = window.appBasePath || "";

    // Ensure basePath ends with a single slash (unless it's just "/")
    const normalizedBase = basePath.endsWith('/') || basePath === '' ? basePath : basePath + '/';

    return normalizedBase + cleanPath;
};