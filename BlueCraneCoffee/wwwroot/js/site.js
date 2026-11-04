(function () {
  var toggle = document.getElementById('navToggle');
  var nav = document.getElementById('mainNav');
  if (toggle && nav) {
    toggle.addEventListener('click', function () {
      var open = nav.classList.toggle('open');
      toggle.setAttribute('aria-expanded', open ? 'true' : 'false');
      toggle.classList.toggle('open', open);
    });
  }
  var toast = document.querySelector('.toast');
  if (toast && toast.scrollIntoView) {
    toast.scrollIntoView({ block: 'nearest' });
  }
})();
