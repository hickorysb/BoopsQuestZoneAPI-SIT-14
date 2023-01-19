import { StaticRouterModService } from "@spt-aki/services/mod/staticRouter/StaticRouterModService";

class GetZonesRoute extends StaticRouterModService {
  public RegisterRoute(): boolean {
    this.AddGetZonesRoute();
    return true;
  }

  private AddGetZonesRoute() {
    // Hook up a new static route
    this.registerStaticRouter(
      "MyStaticModRouter",
      [
        {
          url: "/zones/getZones",
          action: (url, info, sessionId, output) => {
            //logger.info("Custom static route hit");
            return JSON.stringify({ response: "OK" });
          },
        },
      ],
      "custom-static-my-mod"
    );
  }
}

module.exports = GetZonesRoute;
