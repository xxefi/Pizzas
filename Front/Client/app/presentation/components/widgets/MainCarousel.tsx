"use client";

import { FC } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import { Autoplay, Navigation, Pagination, EffectFade } from "swiper/modules";
import { ArrowRight } from "lucide-react";
import { useTranslations } from "next-intl";
import { getCarouselSlides } from "@/app/infrastructure/mock/carouselSlides";

export const MainCarousel: FC = () => {
  const t = useTranslations("Carousel");
  const slides = getCarouselSlides(t);
  return (
    <div className="relative h-[600px] rounded-3xl shadow-2xl overflow-hidden">
      <Swiper
        modules={[Autoplay, Navigation, Pagination, EffectFade]}
        effect="fade"
        speed={1000}
        autoplay={{
          delay: 5000,
          disableOnInteraction: false,
        }}
        navigation={{
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        }}
        pagination={{
          el: ".swiper-pagination",
          clickable: true,
        }}
        className="h-full"
      >
        {slides.map((slide, index) => (
          <SwiperSlide key={index}>
            <div
              className="absolute inset-0 bg-cover bg-center"
              style={{ backgroundImage: `url(${slide.image})` }}
            >
              <div
                className={`absolute inset-0 bg-gradient-to-r ${slide.gradient}`}
              />
              <div className="relative h-full flex items-center px-[5%]">
                <div className="bg-white/10 backdrop-blur-xl rounded-2xl p-10 max-w-2xl border border-white/20 shadow-2xl transform transition-all duration-500 hover:scale-[1.02]">
                  <h1 className="text-5xl font-black text-white mb-4 leading-tight tracking-tight">
                    {slide.title}
                  </h1>
                  <p className="text-xl text-white/90 mb-8 leading-relaxed">
                    {slide.description}
                  </p>
                  <button className="group/btn inline-flex items-center gap-3 bg-white text-gray-900 px-8 py-4 rounded-xl text-lg font-semibold transition-all hover:bg-white/90 hover:gap-4">
                    {slide.buttonText}
                    <ArrowRight className="w-5 h-5 transition-transform group-hover/btn:translate-x-1" />
                  </button>
                </div>
              </div>
            </div>
          </SwiperSlide>
        ))}

        <div className="swiper-button-prev !w-12 !h-12 !bg-white/10 !backdrop-blur-md !rounded-full !transition-all hover:!bg-white/20 hover:!scale-110 after:!hidden" />
        <div className="swiper-button-next !w-12 !h-12 !bg-white/10 !backdrop-blur-md !rounded-full !transition-all hover:!bg-white/20 hover:!scale-110 after:!hidden" />

        <div className="swiper-pagination !bottom-6" />
      </Swiper>
    </div>
  );
};
