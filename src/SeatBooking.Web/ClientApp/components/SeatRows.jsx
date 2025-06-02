import React from 'react';

const SeatRows = ({ seatRows, seatColumns, getSeatClass, handleSeatClick }) => (
  <div className="space-y-2">
    {seatRows.map((row, rowIndex) => (
      <div key={row.rowNumber} className="flex items-center justify-center">
        <div className="flex">
          {seatColumns.map((col, colIndex) => {
            const seat = row.seats[colIndex];
            console.log(seat.code);
            if (!seat || seat.storefrontSlotCode !== "SEAT") {
              // Render blank/aisle/side as empty space
              return <div key={`row${rowIndex}-col${colIndex}`} className="w-8 h-8" />;
            }
            return (
              <button
                key={`row${rowIndex}-col${colIndex}-${seat.code || 'empty'}`}
                className={getSeatClass(seat)}
                onClick={() => handleSeatClick(rowIndex, colIndex)}
                disabled={!seat.available}
                title={seat.code}
              >
                {seat.code}
              </button>
            );
          })}
        </div>
      </div>
    ))}
  </div>
);

export default SeatRows;
