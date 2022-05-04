import { CommodityModel } from "src/core/models/commoditymodel.model";

export interface SnapshotSummary {
    commodityModel?: CommodityModel | null;
    date: Date;
    pnl?: number | null;
    pnlYtd?: number | null;
    pnlLtd?: number | null;
    price?: number | null;
    openPosition?: number | null;
}

export interface Snapshot {
    snapshots: SnapshotSummary[];
}