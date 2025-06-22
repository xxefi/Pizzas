"use client";

import { FC } from "react";
import { Swiper, SwiperSlide } from "swiper/react";
import {
  Autoplay,
  Navigation,
  Pagination,
  EffectFade,
  Parallax,
} from "swiper/modules";
import { ArrowRight, ChevronLeft, ChevronRight } from "lucide-react";
import { useTranslations } from "next-intl";
import { getCarouselSlides } from "@/app/infrastructure/mock/carouselSlides";
import "swiper/css";
import "swiper/css/effect-fade";
import "swiper/css/navigation";
import "swiper/css/pagination";

export const MainCarousel: FC = () => {
  const t = useTranslations("Carousel");
  const slides = getCarouselSlides(t);

  return (
    <div className="relative h-[700px] rounded-[2rem] shadow-[0_20px_50px_rgba(8,_112,_184,_0.7)] overflow-hidden">
      <Swiper
        modules={[Autoplay, Navigation, Pagination, EffectFade, Parallax]}
        effect="fade"
        speed={1500}
        autoplay={{
          delay: 6000,
          disableOnInteraction: false,
        }}
        navigation={{
          nextEl: ".swiper-button-next",
          prevEl: ".swiper-button-prev",
        }}
        pagination={{
          el: ".swiper-pagination",
          clickable: true,
          renderBullet: (index, className) => {
            return `<span class="${className} !w-3 !h-3 !bg-white/50 hover:!bg-white !transition-all !duration-300"></span>`;
          },
        }}
        className="h-full"
      >
        {slides.map((slide, index) => (
          <SwiperSlide key={index}>
            <div
              className="absolute inset-0 bg-cover bg-center transform scale-105 transition-transform duration-[1500ms]"
              style={{ backgroundImage: `url(${slide.image})` }}
            >
              <div
                className={`absolute inset-0 bg-gradient-to-r ${slide.gradient} opacity-90`}
              />
              <div className="relative h-full flex items-center px-[8%]">
                <div className="bg-white/10 backdrop-blur-xl rounded-3xl p-12 max-w-2xl border border-white/20 shadow-2xl transform transition-all duration-700 hover:scale-[1.02] hover:shadow-[0_20px_50px_rgba(8,_112,_184,_0.7)]">
                  <h1 className="text-6xl font-black text-white mb-6 leading-tight tracking-tight animate-fade-in">
                    {slide.title}
                  </h1>
                  <p className="text-2xl text-white/90 mb-10 leading-relaxed animate-fade-in-delay">
                    {slide.description}
                  </p>
                  <button className="group/btn inline-flex items-center gap-4 bg-white text-gray-900 px-10 py-5 rounded-2xl text-xl font-semibold transition-all duration-500 hover:bg-white/90 hover:gap-6 hover:shadow-[0_10px_30px_rgba(8,_112,_184,_0.7)]">
                    {slide.buttonText}
                    <ArrowRight className="w-6 h-6 transition-transform duration-500 group-hover/btn:translate-x-2" />
                  </button>
                </div>
              </div>
            </div>
          </SwiperSlide>
        ))}

        <div className="swiper-button-prev !w-14 !h-14 !bg-white/10 !backdrop-blur-md !rounded-full !transition-all hover:!bg-white/20 hover:!scale-110 after:!hidden">
          <ChevronLeft className="w-8 h-8 text-white" />
        </div>
        <div className="swiper-button-next !w-14 !h-14 !bg-white/10 !backdrop-blur-md !rounded-full !transition-all hover:!bg-white/20 hover:!scale-110 after:!hidden">
          <ChevronRight className="w-8 h-8 text-white" />
        </div>

        <div className="swiper-pagination !bottom-8" />
      </Swiper>
    </div>
  );
};
