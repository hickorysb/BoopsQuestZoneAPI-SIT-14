import { DependencyContainer } from "tsyringe";
import type { IPreAkiLoadMod } from "@spt-aki/models/external/IPreAkiLoadMod";
import type { ILogger } from "@spt-aki/models/spt/utils/ILogger";
import type { DynamicRouterModService } from "@spt-aki/services/mod/dynamicRouter/DynamicRouterModService";
import type { StaticRouterModService } from "@spt-aki/services/mod/staticRouter/StaticRouterModService";
import { PreAkiModLoader } from "@spt-aki/loaders/PreAkiModLoader";

class Mod implements IPreAkiLoadMod {
  public preAkiLoad(container: DependencyContainer): void {
    const loader = container.resolve<PreAkiModLoader>("PreAkiModLoader");
    const logger = container.resolve<ILogger>("WinstonLogger");
    const staticRouterModService = container.resolve<StaticRouterModService>(
      "StaticRouterModService"
    );

    const apiPath = loader.getModPath("API");
    const api = require(apiPath);
  }
}
module.exports = { mod: new Mod() };
