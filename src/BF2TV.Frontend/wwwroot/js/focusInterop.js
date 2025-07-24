// wwwroot/focusInterop.js
window.focusInterop = {
    registerFocusHandlers: function (dotNetHelper) {
        window.addEventListener("blur", () => {
            dotNetHelper.invokeMethodAsync("OnWindowBlur");
        });

        window.addEventListener("focus", () => {
            dotNetHelper.invokeMethodAsync("OnWindowFocus");
        });
    }
};
