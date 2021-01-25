import sys
import pygame
from pygame.locals import QUIT, Rect

pygame.init()
SURFACE = pygame.display.set_mode((400, 300))
FPSCLOCK = pygame.time.Clock()


def main():
    """main routine"""

    while True:
        for event in pygame.event.get():
            if event.type == QUIT:
                pygame.quit()
                sys.exit()

        SURFACE.fill((255, 255, 255))

        pygame.draw.rect(SURFACE, (255, 0, 0), (10, 20, 100, 50))
        pygame.draw.rect(SURFACE, (255, 0, 0), (150, 10, 100, 30), 3)
        pygame.draw.rect(SURFACE, (0, 255, 0), ((100, 80), (80, 50)))

        rect0 = Rect(200, 60, 140, 80)
        pygame.draw.rect(SURFACE, (0, 0, 255), rect0)

        rect1 = Rect((30, 160), (100, 50))
        pygame.draw.rect(SURFACE, (255, 255, 0), rect1)

        # circle( Surface, color, pos, radius, width = 0) -> Rect
        pygame.draw.circle(SURFACE, (255, 0, 0), (150, 50), 20, 10)

        # ellipse( Surface, color, Rect, width = 0) -> Rect
        pygame.draw.ellipse(SURFACE, (0, 255, 0), (50, 150, 110, 60), 5)

        # line( Surface, color, start_ pos, end_ pos, width = 1) -> Rect
        # 青： 斜線（ 太 さ 10）
        start_pos = (300, 30)
        end_pos = (380, 200)
        pygame.draw.line(SURFACE, (0, 0, 255), start_pos, end_pos, 10)

        pygame.display.update()
        FPSCLOCK.tick(3)


if __name__ == '__main__':
    print(sys.version)
    print(sys.path)
    main()
