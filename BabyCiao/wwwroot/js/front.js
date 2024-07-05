"use strict";

document.addEventListener("DOMContentLoaded", function () {
    // ------------------------------------------------------- //
    // Transition Placeholders
    // ------------------------------------------------------ //
    let materialInputs = document.querySelectorAll("input.input-material");
    let materialLabel = document.querySelectorAll("label.label-material");

    // activate labels for prefilled values
    let filledMaterialInputs = Array.from(materialInputs).filter(function (input) {
        return input.value !== "";
    });
    filledMaterialInputs.forEach((input) => input.parentElement.lastElementChild.setAttribute("class", "label-material active"));

    // move label on focus
    materialInputs.forEach((input) => {
        input.addEventListener("focus", function () {
            input.parentElement.lastElementChild.setAttribute("class", "label-material active");
        });
    });

    // remove/keep label on blur
    materialInputs.forEach((input) => {
        input.addEventListener("blur", function () {
            if (input.value !== "") {
                input.parentElement.lastElementChild.setAttribute("class", "label-material active");
            } else {
                input.parentElement.lastElementChild.setAttribute("class", "label-material");
            }
        });
    });

    // ------------------------------------------------------- //
    // Footer
    // ------------------------------------------------------ //
    let footer = document.querySelector("#footer");
    if (footer) {
        document.addEventListener("sidebarChanged", function () {
            adjustFooter();
        });
        window.addEventListener("resize", function () {
            adjustFooter();
        });
    }

    function adjustFooter() {
        if (footer) {
            var footerBlockHeight = footer.offsetHeight;
            var pageContent = document.querySelector(".page-content");
            if (pageContent) {
                pageContent.style.paddingBottom = `${footerBlockHeight}px`;
            }
        }
    }

    // ------------------------------------------------------- //
    // Card Close
    // ------------------------------------------------------ //
    const closeCardBtn = document.querySelectorAll(".card-close a.remove");
    closeCardBtn.forEach((el) => {
        el.addEventListener("click", (e) => {
            e.preventDefault();
            el.closest(".card").style.opacity = "0";
            setTimeout(function () {
                el.closest(".card").classList.add("d-none");
            }, 300);
        });
    });

    // ------------------------------------------------------- //
    // Card Close dropdown
    // ------------------------------------------------------ //
    const cardSettingsToggle = document.querySelectorAll(".card-close .dropdown-toggle");
    cardSettingsToggle.forEach((el) => {
        el.addEventListener("click", () => {
            if (el.classList.contains("show")) {
                setTimeout(function () {
                    el.nextElementSibling.classList.add("is-visible");
                }, 100);
            }
        });
    });

    document.addEventListener("click", function (e) {
        cardSettingsToggle.forEach((el) => {
            if (e.target == el) {
                setTimeout(function () {
                    el.nextElementSibling.classList.add("is-visible");
                }, 100);
            } else {
                el.nextElementSibling.classList.remove("is-visible");
            }
        });
    });

    // ------------------------------------------------------- //
    // Search Popup
    // ------------------------------------------------------ //
    var searchOpenBtn = document.querySelector(".search-open");
    var searchPanel = document.querySelector(".search-panel");
    var searchCloseBtn = document.querySelector(".search-panel .close-btn");
    if (searchOpenBtn) {
        searchOpenBtn.addEventListener("click", function (e) {
            e.preventDefault();
            if (searchPanel) {
                searchPanel.style.display = "block";
            }
        });
        if (searchCloseBtn) {
            searchCloseBtn.addEventListener("click", function (e) {
                e.preventDefault();
                if (searchPanel) {
                    searchPanel.style.display = "none";
                }
            });
        }
    }

    // ------------------------------------------------------- //
    // Tooltips init
    // ------------------------------------------------------ //
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // ------------------------------------------------------- //
    // Sidebar Functionality
    // ------------------------------------------------------ //
    var sidebarToggler = document.querySelector(".sidebar-toggle");
    var sidebar = document.querySelector("#sidebar");
    var pageContent = document.querySelector(".page-content");
    var navBrand = document.querySelector(".navbar-brand");
    if (sidebarToggler) {
        sidebarToggler.addEventListener("click", function () {
            this.classList.toggle("active");
            if (navBrand) navBrand.classList.toggle("active");

            if (sidebar) sidebar.classList.toggle("shrinked");
            if (pageContent) pageContent.classList.toggle("active");
            document.dispatchEvent(new Event("sidebarChanged"));
        });
    }

    function bsValidationBehavior(errorInputs, form) {
        function watchError() {
            errorInputs.forEach((input) => {
                if (input.classList.contains("js-validate-error-field")) {
                    input.classList.add("is-invalid");
                    input.classList.remove("is-valid");
                } else {
                    input.classList.remove("is-invalid");
                    input.classList.add("is-valid");
                }
            });
        }
        watchError();
    }

    // ------------------------------------------------------- //
    // Login Form Validation
    // ------------------------------------------------------ //
    let loginForm = document.querySelector(".login-form");
    if (loginForm) {
        new window.JustValidate(".login-form", {
            rules: {
                loginUsername: {
                    required: true,
                    email: true,
                },
                loginPassword: {
                    required: true,
                },
            },
            messages: {
                loginUsername: "請輸入信箱",
                loginPassword: "請輸入密碼",
            },
            invalidFormCallback: function () {
                let errorInputs = document.querySelectorAll(".login-form input[required]");
                bsValidationBehavior(errorInputs, loginForm);
                loginForm.addEventListener("keyup", () => bsValidationBehavior(errorInputs, loginForm));
            },
        });
    }

    // ------------------------------------------------------- //
    // Register Form Validation
    // ------------------------------------------------------ //
    let registerForm = document.querySelector(".register-form");
    if (registerForm) {
        new window.JustValidate(".register-form", {
            rules: {
                registerUsername: {
                    required: true,
                },
                registerEmail: {
                    required: true,
                    email: true,
                },
                registerPassword: {
                    required: true,
                },
                registerAgree: {
                    required: true,
                },
            },
            messages: {
                registerUsername: "請輸入用戶名",
                registerEmail: "請輸入信箱",
                registerPassword: "請輸入密碼",
                registerAgree: "請同意條款和政策",
            },
            invalidFormCallback: function () {
                let errorInputs = document.querySelectorAll(".register-form input[required]");
                bsValidationBehavior(errorInputs, registerForm);
                registerForm.addEventListener("keyup", () => bsValidationBehavior(errorInputs, registerForm));
                registerForm.addEventListener("change", () => bsValidationBehavior(errorInputs, registerForm));
            },
        });
    }

    // ------------------------------------------------------- //
    // Profile page choices
    // ------------------------------------------------------ //
    function injectClassess(x) {
        let pickerCustomClass = x.dataset.customclass;
        let pickerSevClasses = pickerCustomClass.split(" ");
        x.parentElement.classList.add.apply(x.parentElement.classList, pickerSevClasses);
    }

    const profileCountryChoices = document.querySelector(".profile-country-choices");
    if (profileCountryChoices) {
        const countryChoices = new Choices(profileCountryChoices, {
            searchEnabled: false,
            placeholder: false,
            callbackOnInit: () => injectClassess(profileCountryChoices),
        });
    }

    // ------------------------------------------------------- //
    // Masonry with ImagesLoaded
    // ------------------------------------------------------ //
    const masonryGrid = document.querySelector(".msnry-grid");
    if (masonryGrid) {
        var msnry = new Masonry(masonryGrid, {
            percentPosition: true,
        });
        imagesLoaded(masonryGrid).on("progress", function () {
            msnry.layout();
        });
    }

    // ------------------------------------------------------- //
    // Theme Color Switcher
    // ------------------------------------------------------ //
    var stylesheet = document.getElementById("theme-stylesheet");
    if (stylesheet) {
        var alternateStylesheet = document.createElement("link");
        alternateStylesheet.setAttribute("id", "new-stylesheet");
        alternateStylesheet.setAttribute("rel", "stylesheet");
        stylesheet.parentNode.insertBefore(alternateStylesheet, stylesheet.nextSibling);

        var styleSwitcher = document.getElementById("colour");
        if (styleSwitcher) {
            styleSwitcher.addEventListener("change", function () {
                var e = styleSwitcher.value;
                alternateStylesheet.setAttribute("href", e);
                Cookies.set("switcherColor", e, { expires: 365, path: "/" });
            });

            var theCookie = Cookies.get("switcherColor");
            if (theCookie) {
                alternateStylesheet.setAttribute("href", theCookie);
            }
        }
    }
});
