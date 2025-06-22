import Image from "next/image";

export const PizzaImage = ({
  imageUrl,
  name,
}: {
  imageUrl: string;
  name: string;
}) => (
  <div className="relative h-[400px] md:h-[500px] rounded-2xl overflow-hidden">
    <Image
      src={imageUrl}
      alt={name}
      layout="fill"
      className="object-cover hover:scale-105 transition-transform duration-500"
      sizes="(max-width: 768px) 100vw, (max-width: 1200px) 50vw, 33vw"
    />
  </div>
);
