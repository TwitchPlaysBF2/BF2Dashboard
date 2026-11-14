import './bootstrap-switch-button.min.js';

// noinspection JSUnusedGlobalSymbols
export function createSwitchButton(switchBtnContainer, switchBtnOption) {
    const DEFAULTS = {
        onlabel: 'On',
        onstyle: 'primary',
        offlabel: 'Off',
        offstyle: 'light',
        size: '',
        style: '',
        width: null,
        height: null,
    };
    switchBtnOption = switchBtnOption || {};
    let options = {
        onlabel: switchBtnOption.onlabel || DEFAULTS.onlabel,
        onstyle: switchBtnOption.onstyle || DEFAULTS.onstyle,
        offlabel: switchBtnOption.offlabel || DEFAULTS.offlabel,
        offstyle: switchBtnOption.offstyle || DEFAULTS.offstyle,
        size: switchBtnOption.size || DEFAULTS.size,
        style: switchBtnOption.style || DEFAULTS.style,
        width: switchBtnOption.width || DEFAULTS.width,
        height: switchBtnOption.height || DEFAULTS.height,
    }

    let input = document.createElement('input');
    input.setAttribute('type', 'checkbox');
    input.setAttribute('data-toggle', 'switchbutton')
    input.setAttribute('data-onlabel', options.onlabel);
    input.setAttribute('data-onstyle', options.onstyle);
    input.setAttribute('data-offlabel', options.offlabel);
    input.setAttribute('data-offstyle', options.offstyle);
    input.setAttribute('data-style', options.style);
    input.setAttribute('data-size', options.size);
    if (!!options.width) {
        input.setAttribute('data-width', options.width);
    }
    if (!!options.height) {
        input.setAttribute('data-height', options.height);
    }

    switchBtnContainer.appendChild(input);
    // noinspection JSUnresolvedFunction
    input.switchButton(options);
    return input;
}

// noinspection JSUnusedGlobalSymbols
export function setSwitchButtonStatus(checkBoxInput, status, preventEventPropagate) {
    // noinspection JSUnresolvedFunction
    checkBoxInput.switchButton(status, preventEventPropagate);
}

let dotnetInvokeRef = function () {
    // noinspection JSUnusedGlobalSymbols
    return {
        init: function (dotnetInvokeReference, checkBoxReference) {
            checkBoxReference.addEventListener('change', function (event) {
                event.value = !!checkBoxReference.checked;
                // noinspection JSUnresolvedFunction
                dotnetInvokeReference.invokeMethodAsync('SwitchBtnEventHandler', event)
            });
        }
    };
}

// noinspection JSUnusedGlobalSymbols
export let createDotNetInvokeRef = function () {
    return new dotnetInvokeRef();
};
