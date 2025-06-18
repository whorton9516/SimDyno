import { Stage, Layer, Rect } from 'react-konva'
import './canvas.css'
import { useState, useEffect, useRef } from 'react';

const VIRTUAL_WIDTH = 1000
const VIRTUAL_HEIGHT = 450

const Canvas = () => {
  const [stageSize, setStageSize] = useState({
    width: VIRTUAL_WIDTH,
    height: VIRTUAL_HEIGHT,
    scale: 1,
  })

  // Reference to the container element
  const containerRef = useRef<HTMLDivElement>(null);
  
  useEffect(() => {
    const updateSize = () => {
      if (!containerRef.current) return;
      const containerWidth = containerRef.current.offsetWidth;
      const containerHeight = containerRef.current.offsetHeight;
      const scaleWidth = containerWidth / VIRTUAL_WIDTH;
      const scaleHeight = containerHeight / VIRTUAL_HEIGHT;
      const scale = Math.min(scaleWidth, scaleHeight);
      setStageSize({
        width: VIRTUAL_WIDTH * scale,
        height: VIRTUAL_HEIGHT * scale,
        scale: scale
      });
    };

    updateSize();
    let resizeObserver: ResizeObserver | null = null;
    if (containerRef.current) {
      resizeObserver = new ResizeObserver(() => {
        updateSize();
      });
      resizeObserver.observe(containerRef.current);
    }
    return () => {
      if (resizeObserver && containerRef.current) {
        resizeObserver.unobserve(containerRef.current);
      }
    };
  }, []);

  return (
    <div
      className="canvas-container"
      ref={containerRef}
      style={{ width: '100%', height: '100%' }}
    >
      <Stage
        width={stageSize.width}
        height={stageSize.height}
        scaleX={stageSize.scale}
        scaleY={stageSize.scale}
      >
        <Layer>
          <Rect
            x={50}
            y={50}
            width={100}
            height={100}
            fill="#29b6f6"
            draggable
          />
        </Layer>
      </Stage>
    </div>
  )
}

export default Canvas