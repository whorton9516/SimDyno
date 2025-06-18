import React, { useEffect, useRef } from "react";
// import { useSignalR } from "../../../services/SignalRContext";
// If you have a SignalRService, import it like this:
import { SignalRService } from "../../../../services/SignalRService";

const Tachometer: React.FC = () => {
  const service = new SignalRService("http://localhost:5000/simdynohub");

  return <div>Tachometer</div>;
};

export { Tachometer };