﻿using CTeleport.Distance.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;


WebHost.Start(routes => routes
    .MapGet("/", async c => await c.Response.WriteAsync("To measure distance in miles between two airports (IATA code) use GET between/{from}&{to}"))
    .MapGet("between/{from}&{to}", async (req, res, data) =>
    {
        // input validation
        if (!data.Values.TryGetValue("from", out IATA codeFrom))
        {
            await res.BadRequest(IATA.InvalidMessage("from"));
            return;
        }

        if (!data.Values.TryGetValue("to", out IATA codeTo))
        {
            await res.BadRequest(IATA.InvalidMessage("to"));
            return;
        }

        // processing
        using var client = new PlacesClient();
        var apFrom = await client.GetAirport(codeFrom);
        var apTo = await client.GetAirport(codeTo);

        var distance = new Distance(apFrom.Location, apTo.Location);

        // format output
        await res.WriteAsync($"{distance.InMiles:F}");
    })
);

Console.ReadKey();
