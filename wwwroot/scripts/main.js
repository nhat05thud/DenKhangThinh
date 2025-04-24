(function ($) {
  $(function () {
    initOwlCarousel();
    initOwlCarouselWithThumb();
    nonBreakSpaceP();
    Fancybox.bind("[data-fancybox]", {
        // Your custom options
    });
  });
})(jQuery);
var mainSlide = $(".main-product-slide");
var detailSlide = $(".detail-product-slide");

$(window).on("resize", function () {
  if ($(window).width() > 1024) {
    $(".wrap-menu").attr("style", "");
    $(".menu-mo").removeClass("open");
  } else {
      $(".wrap-menu").removeClass("open");
    $(".wrap-menu").css("right", "");
  }
});
$(".menu-mo").click(function (e) {
  e.preventDefault();
    $(this).stop(true, false, true).toggleClass("open");
    $(".wrap-menu").stop(true, false, true).toggleClass("open");
});

function initOwlCarousel() {
  var mainProductSlide = $(".main-products-slide");
  if (mainProductSlide.length > 0) {
    mainProductSlide.owlCarousel({
      margin: 30,
      slideSpeed: 2000,
      nav: false,
      autoplay: true,
      dots: false,
      loop: true,
      navText: [
        '<svg width="30px" height="30px" viewBox="0 0 16.58 19.15"><polygon fill="#f3e5d8" points="0 9.57 16.58 19.15 16.58 0 0 9.57"/></svg>',
        '<svg width="30px" height="30px" viewBox="0 0 16.78 19.38"><polygon fill="#f3e5d8" points="16.78 9.69 0 0 0 19.38 16.78 9.69"/></svg>',
      ],
      responsive: {
        0: {
          items: 1,
        },
        640: {
          items: 2,
        },
        992: {
          items: 4,
        },
      },
    });
  }
}
function initOwlCarouselWithThumb() {
  if (mainSlide.length <= 0) {
    return;
  }
  var slidesPerPage = 5;

  mainSlide
    .owlCarousel({
      items: 1,
      slideSpeed: 2000,
      nav: false,
      autoplay: false,
      dots: false,
      loop: false,
      responsiveRefreshRate: 200,
    })
    .on("changed.owl.carousel", syncPosition);

  detailSlide
    .on("initialized.owl.carousel", function () {
      detailSlide.find(".owl-item").eq(0).addClass("current");
    })
    .owlCarousel({
      margin: 15,
      items: slidesPerPage,
      dots: false,
      nav: false,
      smartSpeed: 200,
      slideSpeed: 500,
      slideBy: slidesPerPage,
      responsiveRefreshRate: 100,
    })
    .on("changed.owl.carousel", syncPosition2);

  detailSlide.on("click", ".owl-item", function (e) {
    e.preventDefault();
    var number = $(this).index();
    mainSlide.data("owl.carousel").to(number, 300, true);
  });
}
function syncPosition(el) {
  var count = el.item.count - 1;
  var current = Math.round(el.item.index - el.item.count / 2 - 0.5);

  if (current < 0) {
    current = count;
  }
  if (current > count) {
    current = 0;
  }
  detailSlide
    .find(".owl-item")
    .removeClass("current")
    .eq(current)
    .addClass("current");
  var onscreen = detailSlide.find(".owl-item.active").length - 1;
  var start = detailSlide.find(".owl-item.active").first().index();
  var end = detailSlide.find(".owl-item.active").last().index();

  if (current > end) {
    detailSlide.data("owl.carousel").to(current, 100, true);
  }
  if (current < start) {
    detailSlide.data("owl.carousel").to(current - onscreen, 100, true);
  }
}
function syncPosition2(el) {
  var number = el.item.index;
  mainSlide.data("owl.carousel").to(number, 100, true);
}
function nonBreakSpaceP() {
  var item = $(".wrap-content").find("p");
  $(item).each(function () {
    if (!$(this).html()) {
      $(this).html("&nbsp;");
    }
  });
}

/* cust slide */
var igt = 0;
setTimeout(function () {
  cust_slide();
}, 100);
function cust_slide() {
  if ($(".cust-slider").length > 0) {
    var allI = document.getElementsByClassName("c-s-img");
    if (allI.length - 1 < igt) igt = 0;
    var nowI = allI[igt];
    setTimeout(function () {
      for (i = 0; i < allI.length; i++) {
        allI[i].classList.remove("vis");
      }
      nowI.classList.toggle("scaled");
      nowI.classList.add("vis");
    }, 0);
    igt++;
    setTimeout(cust_slide, 8000);
  }
}
/* end cust slide */

addEventListener(
  "load",
  function () {
    if ($(".base_overflow").length > 0) {
      $(".base_overflow").height(400);
    }
  },
  false
);

addEventListener(
  "resize",
  function () {
    if ($(".base_overflow").length > 0) {
        $(".base_overflow").height(400);
    }
  },
  false
);
