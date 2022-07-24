function isMobile() {
    var ua = navigator.userAgent; /* navigator.userAgent 浏览器发送的用户代理标题 */
    if (ua.indexOf('Android') > -1 || ua.indexOf('iPhone') > -1 || ua.indexOf('iPod') > -1 || ua.indexOf('Symbian') > -1) {
        return 'true';
    } else {
        return 'false';
    }
};
