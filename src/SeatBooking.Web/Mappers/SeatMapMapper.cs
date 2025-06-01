using SeatBooking.Domain.AircraftAggregate;
using SeatBooking.Web.Models;

namespace SeatBooking.Web.Mappers;

public static class AircraftMapper
{
    public static Aircraft ToDomain(this SeatMapDto dto)
    {
        // Defensive: ensure required fields are present
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (string.IsNullOrWhiteSpace(dto.Aircraft)) throw new ArgumentException("Aircraft code cannot be null or empty.", nameof(dto.Aircraft));

        // Create Aircraft
        var aircraft = new Aircraft(dto.Aircraft, dto.Aircraft);

        // Map cabins
        if (dto.Cabins != null)
        {
            foreach (var cabinDto in dto.Cabins)
            {
                var cabin = new Cabin(cabinDto.Deck ?? string.Empty, dto.Aircraft);

                // Map seat rows
                if (cabinDto.SeatRows != null)
                {
                    foreach (var rowDto in cabinDto.SeatRows)
                    {
                        var seatRow = new SeatRow(rowDto.RowNumber ?? 0, cabin.Id);

                        // Map seat slots
                        if (rowDto.Seats != null)
                        {
                            foreach (var seatDto in rowDto.Seats)
                            {
                                var seatSlot = new SeatSlot(
                                    seatDto.StorefrontSlotCode ?? string.Empty,
                                    seatDto.Code ?? string.Empty,
                                    seatDto.Available ?? false,
                                    seatDto.Entitled ?? false,
                                    seatDto.FreeOfCharge ?? false,
                                    seatDto.FeeWaived ?? false,
                                    seatDto.OriginallySelected ?? false,
                                    seatDto.EntitledRuleId ?? string.Empty,
                                    seatDto.FeeWaivedRuleId ?? string.Empty,
                                    seatDto.RefundIndicator ?? string.Empty
                                );

                                // Map designations
                                if (seatDto.Designations != null)
                                {
                                    foreach (var designation in seatDto.Designations.OfType<string>())
                                        seatSlot.AddDesignation(new SlotDesignation(designation));
                                }

                                // Map limitations
                                if (seatDto.Limitations != null)
                                {
                                    foreach (var limitation in seatDto.Limitations.OfType<string>())
                                        seatSlot.AddLimitation(new SlotLimitation(limitation));
                                }

                                // Map seat characteristics
                                if (seatDto.SeatCharacteristics != null)
                                {
                                    foreach (var ch in seatDto.SeatCharacteristics)
                                        seatSlot.AddSeatCharacteristic(new SeatCharacteristic(ch, false));
                                }

                                // Map raw seat characteristics
                                if (seatDto.RawSeatCharacteristics != null)
                                {
                                    foreach (var ch in seatDto.RawSeatCharacteristics)
                                        seatSlot.AddSeatCharacteristic(new SeatCharacteristic(ch, true));
                                }

                                // Map prices
                                if (seatDto.Prices?.Alternatives != null)
                                {
                                    foreach (var alt in seatDto.Prices.Alternatives)
                                    {
                                        var priceAlt = new SeatPriceAlternative(seatSlot.Id);
                                        foreach (var amount in alt)
                                            priceAlt.AddComponent(new SeatPriceComponent((decimal)(amount.Amount ?? 0), amount.Currency ?? string.Empty));
                                        seatSlot.AddPriceAlternative(priceAlt);
                                    }
                                }

                                // Map taxes
                                if (seatDto.Taxes?.Alternatives != null)
                                {
                                    foreach (var alt in seatDto.Taxes.Alternatives)
                                    {
                                        var taxAlt = new SeatTaxAlternative(seatSlot.Id);
                                        foreach (var amount in alt)
                                            taxAlt.AddComponent(new SeatTaxComponent((decimal)(amount.Amount ?? 0), amount.Currency ?? string.Empty));
                                        seatSlot.AddTax(taxAlt);
                                    }
                                }

                                seatRow.AddSeatSlot(seatSlot);
                            }
                        }

                        cabin.AddSeatRow(seatRow);
                    }
                }

                // Map seat columns
                if (cabinDto.SeatColumns != null)
                {
                    foreach (var colCode in cabinDto.SeatColumns)
                    {
                        var seatColumn = new SeatColumn(colCode, cabin.Id);
                        cabin.AddSeatColumn(seatColumn);
                    }
                }

                aircraft.AddCabin(cabin);
            }
        }

        return aircraft;
    } 

    public static SeatMapDto ToDto(this Aircraft aircraft)
    {
        return new SeatMapDto(
            RowsDisabledCauses: new List<object>(), // Map as needed
            Aircraft: aircraft.Code,
            Cabins: aircraft.Cabins.Select(cabin => new CabinDto(
                Deck: cabin.Deck,
                SeatColumns: cabin.SeatColumns.Select(col => col.Code).ToList(),
                SeatRows: cabin.SeatRows.Select(row => new SeatRowDto(
                    RowNumber: row.RowNumber,
                    SeatCodes: row.SeatSlots.Select(slot => slot.Code ?? string.Empty).ToList(),
                    Seats: row.SeatSlots.Select(slot => new SeatDto(
                        SlotCharacteristics: slot.SeatCharacteristics.Select(sc => sc.Code).ToList(),
                        StorefrontSlotCode: slot.StorefrontSlotCode,
                        Available: slot.Available,
                        Entitled: slot.Entitled,
                        FeeWaived: slot.FeeWaived,
                        FreeOfCharge: slot.FreeOfCharge,
                        OriginallySelected: slot.OriginallySelected,
                        Code: slot.Code,
                        Designations: slot.Designations.Select(d => d.Code).Cast<object>().ToList(),
                        EntitledRuleId: slot.EntitledRuleId,
                        FeeWaivedRuleId: slot.FeeWaivedRuleId,
                        SeatCharacteristics: slot.SeatCharacteristics.Select(sc => sc.Code).ToList(),
                        Limitations: slot.Limitations.Select(l => l.Code).Cast<object>().ToList(),
                        RefundIndicator: slot.RefundIndicator,
                        Prices: slot.PriceAlternatives.Any() ? new PriceAlternativesDto(
                            Alternatives: slot.PriceAlternatives.Select(pa =>
                                pa.Components.Select(comp => new PriceDto(
                                    Amount: (double?)comp.Amount,
                                    Currency: comp.Currency
                                )).ToList()
                            ).ToList()
                        ) : null,
                        Taxes: slot.Taxes.Any() ? new PriceAlternativesDto(
                            Alternatives: slot.Taxes.Select(tax =>
                                tax.Components.Select(comp => new PriceDto(
                                    Amount: (double?)comp.Amount,
                                    Currency: comp.Currency
                                )).ToList()
                            ).ToList()
                        ) : null,
                        // Total: slot.Total.Any() ? new PriceAlternativesDto(
                        //     Alternatives: new List<List<PriceDto>> {
                        //         slot.Total.Select(comp => new PriceDto(
                        //             Amount: (double?)comp.Amount,
                        //             Currency: comp.Currency
                        //         )).ToList()
                        //     }
                        // ) : null,
                        Total: null,
                        RawSeatCharacteristics: slot.SeatCharacteristics.Where(sc => sc.IsRaw).Select(sc => sc.Code).ToList()
                    )).ToList()
                )).ToList()
            )).ToList()
        );
    }
}