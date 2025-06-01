import React from "react";

const SeatRows = ({ seatRows, getSeatClass, getSeatCurrency , handleSeatClick, getSeatPrice}) => (
  <div className="space-y-2">
    {seatRows.map((row, rowIndex) => (
      <div key={row.rowNumber} className="flex items-center justify-center">
        <div className="flex">
          {/* Left side seats (A, B, C) */}
          {row.seats.slice(0, 3).map((seat, seatIndex) => (
            <button
              key={seat.code}
              className={getSeatClass(seat)}
              onClick={() => handleSeatClick(rowIndex, seatIndex)}
              disabled={!seat.available}
              title={`${seat.code} - ${getSeatCurrency(seat)} ${getSeatPrice(
                seat
              )}`}
            >
              {seat.code.slice(-1)}
            </button>
          ))}
          {/* Row number cell */}
          <span className="w-8 h-8 flex items-center justify-center text-sm font-medium text-gray-600">
            {row.rowNumber}
          </span>
          {/* Right side seats (D, E, F) */}
          {row.seats.slice(3, 6).map((seat, seatIndex) => (
            <button
              key={seat.code}
              className={getSeatClass(seat)}
              onClick={() => handleSeatClick(rowIndex, seatIndex + 3)}
              disabled={!seat.available}
              title={`${seat.code} - ${getSeatCurrency(seat)} ${getSeatPrice(
                seat
              )}`}
            >
              {seat.code.slice(-1)}
            </button>
          ))}
        </div>
      </div>
    ))}
  </div>
);

export default SeatRows;
