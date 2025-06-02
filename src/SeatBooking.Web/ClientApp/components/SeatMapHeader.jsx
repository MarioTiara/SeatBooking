import React from "react";

const SeatMapHeader = ({ seatColumns }) => {
  return (
    <div className="flex">
      {seatColumns.map((col, idx) => (
        <div key={idx} className="w-8 h-8 flex items-center justify-center font-bold">
          {["LEFT_SIDE", "AISLE", "RIGHT_SIDE"].includes(col) ? "" : col}
        </div>
      ))}
    </div>
  );
};

export default SeatMapHeader;
