import React from "react";

const SeatMapHeader = ({seatColumns}) => (
  <div className="flex justify-center mb-4">
    <div className="flex">
      {/* Left seats (A, B, C) */}
      {seatColumns.slice(0, 3).map((col) => (
        <span
          key={col}
          className="w-8 h-8 flex items-center justify-center text-xs font-medium text-gray-500"
        >
          {col}
        </span>
      ))}
      {/* Row number placeholder for alignment */}
      <span className="w-8 h-8" />
      {/* Right seats (D, E, F) */}
      {seatColumns.slice(3, 6).map((col) => (
        <span
          key={col}
          className="w-8 h-8 flex items-center justify-center text-xs font-medium text-gray-500"
        >
          {col}
        </span>
      ))}
    </div>
  </div>
);

export default SeatMapHeader;
