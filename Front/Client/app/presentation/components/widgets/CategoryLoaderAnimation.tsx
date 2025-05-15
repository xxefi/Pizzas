import { Div } from "@vkontakte/vkui";
import React from "react";
import { Card } from "rsuite";

export default function CategoryLoaderAnimation() {
  return (
    <Div className="py-12">
      <div className="max-w-7xl mx-auto px-4">
        <div className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4">
          {Array(10)
            .fill(0)
            .map((_, index) => (
              <div key={index} className="animate-pulse">
                <Card className="p-4 text-center">
                  <div className="mx-auto mb-2 size-16 rounded-full bg-gray-200 dark:bg-gray-700"></div>
                  <div className="mx-auto h-4 w-3/4 rounded bg-gray-200 dark:bg-gray-700"></div>
                </Card>
              </div>
            ))}
        </div>
      </div>
    </Div>
  );
}
