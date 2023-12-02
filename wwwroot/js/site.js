document.addEventListener('DOMContentLoaded', function () {
    var labels = document.querySelectorAll('label[for]');
    labels.forEach(function (label) {
        var inputId = label.getAttribute('for');
        var inputField = document.getElementById(inputId);

        if (inputField) {
            if (!inputField.placeholder) {
                inputField.placeholder = "Enter " + label.textContent.trim();
            }
        }
    });
});