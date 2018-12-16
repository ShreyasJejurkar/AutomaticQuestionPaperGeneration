

function DisableBackButton() {
    window.history.forward();
}

DisableBackButton();
window.onload = DisableBackButton;
window.onpageshow = function(evt) {
    if (evt.persisted) DisableBackButton();

};
window.onunload = function() {
    void (0);

};