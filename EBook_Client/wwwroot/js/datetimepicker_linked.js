
(function () {
    // linkedPickers11、12
    const linkedPicker11Element = document.getElementById('linkedPickers11');
    const linked11 = new tempusDominus.TempusDominus(linkedPicker11Element, {
        display: {
            icons: {
                time: 'bi bi-clock',
                date: 'bi bi-calendar',
                up: 'bi bi-arrow-up',
                down: 'bi bi-arrow-down',
                previous: 'bi bi-chevron-left',
                next: 'bi bi-chevron-right',
                today: 'bi bi-calendar-check',
                clear: 'bi bi-trash',
                close: 'bi bi-x',
            },
            buttons: {
                today: true,
                clear: true,
                close: true,
            },
        }
    });
    const linked12 = new tempusDominus.TempusDominus(document.getElementById('linkedPickers12'), {
        display: {
            icons: {
                time: 'bi bi-clock',
                date: 'bi bi-calendar',
                up: 'bi bi-arrow-up',
                down: 'bi bi-arrow-down',
                previous: 'bi bi-chevron-left',
                next: 'bi bi-chevron-right',
                today: 'bi bi-calendar-check',
                clear: 'bi bi-trash',
                close: 'bi bi-x',
            },
            buttons: {
                today: true,
                clear: true,
                close: true,
            },
        },
        useCurrent: false,
    });
    linkedPicker11Element.addEventListener(tempusDominus.Namespace.events.change, (e) => {
        linked12.updateOptions({
            restrictions: {
                minDate: e.detail.date,
            },
        });
    });
    const subscription1 = linked12.subscribe(tempusDominus.Namespace.events.change, (e) => {
        linked11.updateOptions({
            restrictions: {
                maxDate: e.date,
            },
        });
    });

    // linkedPickers21、22
    const linkedPicker21Element = document.getElementById('linkedPickers21');
    const linked21 = new tempusDominus.TempusDominus(linkedPicker21Element, {
        display: {
            icons: {
                time: 'bi bi-clock',
                date: 'bi bi-calendar',
                up: 'bi bi-arrow-up',
                down: 'bi bi-arrow-down',
                previous: 'bi bi-chevron-left',
                next: 'bi bi-chevron-right',
                today: 'bi bi-calendar-check',
                clear: 'bi bi-trash',
                close: 'bi bi-x',
            },
            buttons: {
                today: true,
                clear: true,
                close: true,
            },
        }
    });
    const linked22 = new tempusDominus.TempusDominus(document.getElementById('linkedPickers22'), {
        display: {
            icons: {
                time: 'bi bi-clock',
                date: 'bi bi-calendar',
                up: 'bi bi-arrow-up',
                down: 'bi bi-arrow-down',
                previous: 'bi bi-chevron-left',
                next: 'bi bi-chevron-right',
                today: 'bi bi-calendar-check',
                clear: 'bi bi-trash',
                close: 'bi bi-x',
            },
            buttons: {
                today: true,
                clear: true,
                close: true,
            },
        },
        useCurrent: false,
    });
    linkedPicker21Element.addEventListener(tempusDominus.Namespace.events.change, (e) => {
        linked22.updateOptions({
            restrictions: {
                minDate: e.detail.date,
            },
        });
    });
    const subscription2 = linked22.subscribe(tempusDominus.Namespace.events.change, (e) => {
        linked21.updateOptions({
            restrictions: {
                maxDate: e.date,
            },
        });
    });

    // linkedPickers31、32
    const linkedPicker31Element = document.getElementById('linkedPickers31');
    const linked31 = new tempusDominus.TempusDominus(linkedPicker31Element, {
        display: {
            icons: {
                time: 'bi bi-clock',
                date: 'bi bi-calendar',
                up: 'bi bi-arrow-up',
                down: 'bi bi-arrow-down',
                previous: 'bi bi-chevron-left',
                next: 'bi bi-chevron-right',
                today: 'bi bi-calendar-check',
                clear: 'bi bi-trash',
                close: 'bi bi-x',
            },
            buttons: {
                today: true,
                clear: true,
                close: true,
            },
        }
    });
    const linked32 = new tempusDominus.TempusDominus(document.getElementById('linkedPickers32'), {
        display: {
            icons: {
                time: 'bi bi-clock',
                date: 'bi bi-calendar',
                up: 'bi bi-arrow-up',
                down: 'bi bi-arrow-down',
                previous: 'bi bi-chevron-left',
                next: 'bi bi-chevron-right',
                today: 'bi bi-calendar-check',
                clear: 'bi bi-trash',
                close: 'bi bi-x',
            },
            buttons: {
                today: true,
                clear: true,
                close: true,
            },
        },
        useCurrent: false,
    });
    linkedPicker31Element.addEventListener(tempusDominus.Namespace.events.change, (e) => {
        linked32.updateOptions({
            restrictions: {
                minDate: e.detail.date,
            },
        });
    });
    const subscription3 = linked32.subscribe(tempusDominus.Namespace.events.change, (e) => {
        linked31.updateOptions({
            restrictions: {
                maxDate: e.date,
            },
        });
    });


})()