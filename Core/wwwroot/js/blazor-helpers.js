window.blazorHelpers = {
    scrollToElement: (query) => {
        var element = document.querySelector(query);

        if (element) {
            element.scrollIntoView({
                behavior: 'smooth'
            });
        }
  }
}