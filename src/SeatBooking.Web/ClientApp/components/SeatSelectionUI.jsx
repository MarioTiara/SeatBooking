import React, { useState, useEffect } from "react";
import { Plane } from "lucide-react";
import SeatSelectionHeader from "./SeatSelectionHeader";
import FlightInfo from "./FlightInfo";
import Legend from "./Legend";
import PassengerInfo from "./PassengerInfo";
import SelectedSeatInfo from "./SelectedSeatInfo";
import SeatMapHeader from "./SeatMapHeader";
import SeatRows from "./SeatRows";
import ConfirmationModal from "./ConfirmationModal";

const SeatSelectionUI = () => {
  const [selectedSeat, setSelectedSeat] = useState(null);
  const [seatMapData, setSeatMapData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [isModalOpen, setIsModalOpen] = useState(false);

  // Define loadSeatMap outside useEffect
  const loadSeatMap = async () => {
    try {
      const response = await fetch("/api/SeatMap");
      const jsonData = await response.json();
      const segmentData = jsonData.seatsItineraryParts[0].segmentSeatMaps[0];
      const passengerData = segmentData.passengerSeatMaps[0];
      const seatMapInfo = passengerData.seatMap;
      const flightSegment = segmentData.segment;

      if (jsonData.selectedSeats[0]) {
        console.log("Assigning selected seat");
        setSelectedSeat(jsonData.selectedSeats[0]);
      }

      const seatColumns = seatMapInfo.cabins[0].seatColumns;
      const extendedSeatMap = seatMapInfo.cabins[0].seatRows.map((row) => ({
        ...row,
        seats: row.seats.map((seat) => ({
          ...seat,
          selected: false,
        })),
      }));

      setSeatMapData({
        aircraft: seatMapInfo.aircraft,
        seatColumns: seatColumns,
        seatRows: extendedSeatMap,
        passenger: passengerData.passenger,
        segment: flightSegment,
      });

      setLoading(false);
    } catch (error) {
      console.error("Error loading seat map:", error);
      setLoading(false);
    }
  };

  // Call loadSeatMap on component mount
  useEffect(() => {
    loadSeatMap();
  }, []);

  const handleSeatClick = (rowIndex, seatIndex) => {
    if (!seatMapData) return;

    const seat = seatMapData.seatRows[rowIndex].seats[seatIndex];
    if (!seat.available) return;

    setSeatMapData((prevData) => {
      const newData = { ...prevData };
      newData.seatRows = [...prevData.seatRows];

      // Clear previous selections
      newData.seatRows.forEach((row) => {
        row.seats.forEach((s) => (s.selected = false));
      });

      // Select new seat
      newData.seatRows[rowIndex] = { ...newData.seatRows[rowIndex] };
      newData.seatRows[rowIndex].seats = [...newData.seatRows[rowIndex].seats];
      newData.seatRows[rowIndex].seats[seatIndex] = {
        ...newData.seatRows[rowIndex].seats[seatIndex],
        selected: true,
      };

      return newData;
    });

    setSelectedSeat(seat);
  };

  const getSeatClass = (seat) => {
    let baseClass =
      "w-8 h-8 mx-0.5 rounded-t-lg border-2 cursor-pointer transition-all duration-200 flex items-center justify-center text-xs font-medium ";

    if (selectedSeat.code == seat.code) {
      return (
        baseClass +
        "bg-blue-600 border-blue-800 text-white transform scale-110 shadow-lg"
      );
    }
    if (!seat.available) {
      return (
        baseClass +
        "bg-gray-300 border-gray-400 cursor-not-allowed text-gray-500"
      );
    }
    // Check seat characteristics from JSON data
    const characteristics = seat.seatCharacteristics || [];
    const isWindow = characteristics.includes("W");
    const isAisle = characteristics.includes("W");
    const hasExtraLegroom = characteristics.includes("EL");

    if (hasExtraLegroom || isWindow || isAisle) {
      return (
        baseClass +
        "bg-green-100 border-green-300 text-green-800 hover:bg-green-200"
      );
    }

    return (
      baseClass + "bg-gray-100 border-gray-300 text-gray-700 hover:bg-gray-200"
    );
  };

  const formatDateTime = (dateTimeString) => {
    const date = new Date(dateTimeString);
    return {
      time: date.toLocaleTimeString("en-US", {
        hour: "2-digit",
        minute: "2-digit",
        hour12: false,
      }),
      date: date.toLocaleDateString("en-US", {
        day: "2-digit",
        month: "short",
        year: "numeric",
      }),
    };
  };

  const getSeatPrice = (seat) => {
    console.log("Seat price:", seat.total?.alternatives?.[0]?.[0]?.amount);
    return seat.total?.alternatives?.[0]?.[0]?.amount || 0;
  };

  const getSeatCurrency = (seat) => {
    return seat.total?.alternatives?.[0]?.[0]?.currency || "MYR";
  };

  const handleConfirm = async () => {
    setIsModalOpen(false);

    const passenger = seatMapData.passenger;
    const aircraftCode = seatMapData.aircraft;

    try {
      const res = await fetch("/api/SeatMap/select-seat", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          seatSlotCode: selectedSeat.code,
          aircraftCode: aircraftCode,
          passengerNameNumber: passenger.passengerNameNumber,
        }),
      });

      const data = await res.json();
      console.log(data);
      alert(`Seat ${selectedSeat.code} selected successfully!`);

      // Reload seat map data after successful POST
      loadSeatMap();
    } catch (error) {
      console.error("Error during POST:", error);
      alert("Failed to select seat. Please try again.");
    }
  };
  const handleCancel = () => {
    setIsModalOpen(false);
  };
  const confirmSelection = async () => {
    setIsModalOpen(true);
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <Plane className="w-12 h-12 text-blue-600 animate-bounce mx-auto mb-4" />
          <p className="text-lg text-gray-600">Loading seat map...</p>
        </div>
      </div>
    );
  }

  if (!seatMapData) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <p className="text-lg text-red-600">Error loading seat map data</p>
        </div>
      </div>
    );
  }

  const departureInfo = formatDateTime(seatMapData.segment.departure);
  const arrivalInfo = formatDateTime(seatMapData.segment.arrival);

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <SeatSelectionHeader segment={seatMapData.segment} />

      <div className="max-w-7xl mx-auto px-4 py-6">
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          {/* Flight Info Panel */}
          <div className="lg:col-span-1">
            <FlightInfo
              segment={seatMapData.segment}
              departureInfo={departureInfo}
              arrivalInfo={arrivalInfo}
              aircraft={seatMapData.aircraft}
            />

            {/* Passenger Info */}
            <PassengerInfo passenger={seatMapData.passenger} />
            {/* Legend */}
            <Legend />
          </div>

          {/* Seat Map */}
          <div className="lg:col-span-2">
            <div className="bg-white rounded-lg shadow-sm border p-6">
              <div className="text-center mb-6">
                <div className="inline-block bg-gray-100 rounded-full px-4 py-2 mb-4">
                  <Plane className="w-6 h-6 mx-auto text-gray-600" />
                </div>
                <p className="text-sm text-gray-600 mb-2">Front of Aircraft</p>
              </div>
              <div className="overflow-x-auto">
                <div className="inline-block min-w-full">
                  {/* Column Headers */}
                  <div className="flex justify-center">
                    <SeatMapHeader seatColumns={seatMapData.seatColumns} />
                  </div>
                  {/* Seat Rows */}
                  <SeatRows
                    seatRows={seatMapData.seatRows}
                    seatColumns={seatMapData.seatColumns}
                    getSeatClass={getSeatClass}
                    handleSeatClick={handleSeatClick}
                  />
                </div>
              </div>
              <div className="text-center mt-6">
                <p className="text-sm text-gray-600">Rear of Aircraft</p>
              </div>
            </div>

            {/* Selected Seat Info */}
            {selectedSeat && (
              <SelectedSeatInfo
                selectedSeat={selectedSeat}
                getSeatCurrency={getSeatCurrency}
                getSeatPrice={getSeatPrice}
                onConfirm={confirmSelection}
                onCancel={() => setSelectedSeat(null)}
              />
            )}
            {/* Confirmation Modal */}
            {isModalOpen && (
              <ConfirmationModal
                isOpen={isModalOpen}
                onConfirm={handleConfirm}
                onCancel={handleCancel}
                message={
                  selectedSeat == null
                    ? `Are you sure you want to select seat ${selectedSeat?.code}?`
                    : `Are you sure you want to change seat to ${selectedSeat?.code}?`
                }
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default SeatSelectionUI;
