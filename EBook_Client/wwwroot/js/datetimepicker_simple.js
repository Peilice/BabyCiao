

(function () {
    const picker1 = new tempusDominus
        .TempusDominus(document.getElementById('datetimepicker1'), {
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
    const picker2 = new tempusDominus
        .TempusDominus(document.getElementById('datetimepicker2'), {
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

})()

